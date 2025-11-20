using Abp.AutoMapper;
using WebCore.Restaurants;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Restaurants.Dto;

[AutoMapTo(typeof(Booking))]
public class CreateBookingDto
{
    [Required]
    [StringLength(Booking.MaxCustomerNameLength)]
    public string CustomerName { get; set; }

    [StringLength(Booking.MaxCustomerPhoneLength)]
    public string CustomerPhone { get; set; }

    [StringLength(Booking.MaxCustomerEmailLength)]
    public string CustomerEmail { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }

    [Required]
    public TimeSpan BookingTime { get; set; }

    [Required]
    [Range(1, 20)]
    public int NumberOfGuests { get; set; }

    [StringLength(Booking.MaxSpecialRequestsLength)]
    public string SpecialRequests { get; set; }

    [Required]
    public int RestaurantId { get; set; }

    public int? TableId { get; set; }
}