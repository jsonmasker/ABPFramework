using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Restaurants;

public class Restaurant : FullAuditedEntity<int>
{
    public const int MaxNameLength = 200;
    public const int MaxDescriptionLength = 1000;
    public const int MaxAddressLength = 500;
    public const int MaxPhoneLength = 20;
    public const int MaxEmailLength = 100;

    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    [StringLength(MaxDescriptionLength)]
    public string Description { get; set; }

    [Required]
    [StringLength(MaxAddressLength)]
    public string Address { get; set; }

    [StringLength(MaxPhoneLength)]
    public string Phone { get; set; }

    [StringLength(MaxEmailLength)]
    public string Email { get; set; }

    public int Capacity { get; set; }

    public bool IsActive { get; set; }

    public decimal Rating { get; set; }

    public virtual ICollection<Table> Tables { get; set; }
    public virtual ICollection<Booking> Bookings { get; set; }

    public Restaurant()
    {
        Tables = new HashSet<Table>();
        Bookings = new HashSet<Booking>();
        IsActive = true;
        Rating = 0;
    }
}