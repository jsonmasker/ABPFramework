using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WebCore.Restaurants.Dto;
using System.Threading.Tasks;

namespace WebCore.Restaurants;

public interface IBookingAppService : IApplicationService
{
    Task<PagedResultDto<BookingDto>> GetAllAsync(GetBookingsInput input);
    Task<BookingDto> GetAsync(EntityDto<int> input);
    Task<BookingDto> CreateAsync(CreateBookingDto input);
    Task<BookingDto> UpdateAsync(BookingDto input);
    Task DeleteAsync(EntityDto<int> input);
    Task<BookingDto> ConfirmBookingAsync(EntityDto<int> input);
    Task<BookingDto> CancelBookingAsync(EntityDto<int> input);
    Task<ListResultDto<BookingDto>> GetMyBookingsAsync();
}