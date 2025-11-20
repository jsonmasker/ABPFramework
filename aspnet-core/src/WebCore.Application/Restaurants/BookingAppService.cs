using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using WebCore.Authorization;
using WebCore.Restaurants.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace WebCore.Restaurants;

[AbpAuthorize(PermissionNames.Pages_Bookings)]
public class BookingAppService : AsyncCrudAppService<Booking, BookingDto, int, GetBookingsInput, CreateBookingDto, BookingDto>, IBookingAppService
{
    private readonly IRepository<Restaurant, int> _restaurantRepository;
    private readonly IRepository<Table, int> _tableRepository;

    public BookingAppService(
        IRepository<Booking, int> repository,
        IRepository<Restaurant, int> restaurantRepository,
        IRepository<Table, int> tableRepository)
        : base(repository)
    {
        _restaurantRepository = restaurantRepository;
        _tableRepository = tableRepository;
        CreatePermissionName = PermissionNames.Pages_Bookings_Create;
        UpdatePermissionName = PermissionNames.Pages_Bookings_Edit;
        DeletePermissionName = PermissionNames.Pages_Bookings_Delete;
    }

    public override async Task<BookingDto> CreateAsync(CreateBookingDto input)
    {
        // Validate restaurant exists
        var restaurant = await _restaurantRepository.GetAsync(input.RestaurantId);
        if (!restaurant.IsActive)
        {
            throw new UserFriendlyException("Restaurant is not active");
        }

        // Validate table if specified
        if (input.TableId.HasValue)
        {
            var table = await _tableRepository.GetAsync(input.TableId.Value);
            if (table.RestaurantId != input.RestaurantId)
            {
                throw new UserFriendlyException("Table does not belong to the specified restaurant");
            }
            
            if (!table.IsAvailable)
            {
                throw new UserFriendlyException("Table is not available");
            }
        }

        // Check for booking conflicts
        var conflictingBookings = await Repository.GetAll()
            .Where(b => b.RestaurantId == input.RestaurantId &&
                       b.BookingDate.Date == input.BookingDate.Date &&
                       b.BookingTime == input.BookingTime &&
                       b.Status != BookingStatus.Cancelled &&
                       (input.TableId == null || b.TableId == input.TableId))
            .CountAsync();

        if (conflictingBookings > 0)
        {
            throw new UserFriendlyException("Time slot is already booked");
        }

        var booking = ObjectMapper.Map<Booking>(input);
        booking.UserId = AbpSession.UserId;

        var createdBooking = await Repository.InsertAsync(booking);
        await CurrentUnitOfWork.SaveChangesAsync();

        return MapToEntityDto(createdBooking);
    }

    public async Task<BookingDto> ConfirmBookingAsync(EntityDto<int> input)
    {
        var booking = await Repository.GetAsync(input.Id);
        booking.Status = BookingStatus.Confirmed;
        
        var updatedBooking = await Repository.UpdateAsync(booking);
        return MapToEntityDto(updatedBooking);
    }

    public async Task<BookingDto> CancelBookingAsync(EntityDto<int> input)
    {
        var booking = await Repository.GetAsync(input.Id);
        
        // Only allow cancellation by the user who made the booking or admin
        if (booking.UserId != AbpSession.UserId && !await IsGrantedAsync("Pages.Administration"))
        {
            throw new UserFriendlyException("You can only cancel your own bookings");
        }

        booking.Status = BookingStatus.Cancelled;
        
        var updatedBooking = await Repository.UpdateAsync(booking);
        return MapToEntityDto(updatedBooking);
    }

    public async Task<ListResultDto<BookingDto>> GetMyBookingsAsync()
    {
        if (!AbpSession.UserId.HasValue)
        {
            throw new UserFriendlyException("User not logged in");
        }

        var bookings = await Repository.GetAll()
            .Include(b => b.Restaurant)
            .Include(b => b.Table)
            .Where(b => b.UserId == AbpSession.UserId)
            .OrderByDescending(b => b.BookingDate)
            .ThenByDescending(b => b.BookingTime)
            .ToListAsync();

        return new ListResultDto<BookingDto>(
            ObjectMapper.Map<List<BookingDto>>(bookings)
        );
    }

    protected override IQueryable<Booking> CreateFilteredQuery(GetBookingsInput input)
    {
        return Repository.GetAll()
            .Include(b => b.Restaurant)
            .Include(b => b.Table)
            .Include(b => b.User)
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), 
                x => x.CustomerName.Contains(input.Filter) || 
                     x.CustomerPhone.Contains(input.Filter) ||
                     x.CustomerEmail.Contains(input.Filter))
            .WhereIf(input.Status.HasValue, x => x.Status == input.Status)
            .WhereIf(input.RestaurantId.HasValue, x => x.RestaurantId == input.RestaurantId)
            .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId)
            .WhereIf(input.BookingDateFrom.HasValue, x => x.BookingDate >= input.BookingDateFrom)
            .WhereIf(input.BookingDateTo.HasValue, x => x.BookingDate <= input.BookingDateTo);
    }

    protected override IQueryable<Booking> ApplySorting(IQueryable<Booking> query, GetBookingsInput input)
    {
        return query.OrderBy(input.Sorting ?? "bookingDate desc, bookingTime desc");
    }

    protected override BookingDto MapToEntityDto(Booking entity)
    {
        var dto = base.MapToEntityDto(entity);
        dto.RestaurantName = entity.Restaurant?.Name;
        dto.TableName = entity.Table?.Name;
        dto.UserName = entity.User?.Name;
        return dto;
    }
}