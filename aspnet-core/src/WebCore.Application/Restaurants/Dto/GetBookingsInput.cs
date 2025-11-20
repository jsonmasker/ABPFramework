using Abp.Application.Services.Dto;
using System;

namespace WebCore.Restaurants.Dto;

public class GetBookingsInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
    public BookingStatus? Status { get; set; }
    public int? RestaurantId { get; set; }
    public long? UserId { get; set; }
    public DateTime? BookingDateFrom { get; set; }
    public DateTime? BookingDateTo { get; set; }
}