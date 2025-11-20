using Abp.Domain.Entities.Auditing;
using WebCore.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Restaurants;

public enum BookingStatus
{
    Pending = 0,
    Confirmed = 1,
    Cancelled = 2,
    Completed = 3,
    NoShow = 4
}

public class Booking : FullAuditedEntity<int>
{
    public const int MaxCustomerNameLength = 100;
    public const int MaxCustomerPhoneLength = 20;
    public const int MaxCustomerEmailLength = 100;
    public const int MaxSpecialRequestsLength = 500;

    [Required]
    [StringLength(MaxCustomerNameLength)]
    public string CustomerName { get; set; }

    [StringLength(MaxCustomerPhoneLength)]
    public string CustomerPhone { get; set; }

    [StringLength(MaxCustomerEmailLength)]
    public string CustomerEmail { get; set; }

    public DateTime BookingDate { get; set; }

    public TimeSpan BookingTime { get; set; }

    public int NumberOfGuests { get; set; }

    public BookingStatus Status { get; set; }

    [StringLength(MaxSpecialRequestsLength)]
    public string SpecialRequests { get; set; }

    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }

    public int? TableId { get; set; }
    public virtual Table Table { get; set; }

    public long? UserId { get; set; }
    public virtual User User { get; set; }

    public Booking()
    {
        Status = BookingStatus.Pending;
        BookingDate = DateTime.Now.AddDays(1);
    }
}