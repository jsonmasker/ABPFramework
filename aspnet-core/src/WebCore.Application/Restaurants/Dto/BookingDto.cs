using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WebCore.Restaurants;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Restaurants.Dto;

[AutoMapFrom(typeof(Booking))]
public class BookingDto : FullAuditedEntityDto<int>
{
    [Required]
    [StringLength(Booking.MaxCustomerNameLength)]
    public string CustomerName { get; set; }

    [StringLength(Booking.MaxCustomerPhoneLength)]
    public string CustomerPhone { get; set; }

    [StringLength(Booking.MaxCustomerEmailLength)]
    public string CustomerEmail { get; set; }

    public DateTime BookingDate { get; set; }

    public TimeSpan BookingTime { get; set; }

    public int NumberOfGuests { get; set; }

    public BookingStatus Status { get; set; }

    [StringLength(Booking.MaxSpecialRequestsLength)]
    public string SpecialRequests { get; set; }

    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }

    public int? TableId { get; set; }
    public string TableName { get; set; }

    public long? UserId { get; set; }
    public string UserName { get; set; }
}