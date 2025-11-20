using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Restaurants;

public class Table : FullAuditedEntity<int>
{
    public const int MaxNameLength = 50;

    [Required]
    [StringLength(MaxNameLength)]
    public string Name { get; set; }

    public int Capacity { get; set; }

    public bool IsAvailable { get; set; }

    public int RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; }

    public Table()
    {
        Bookings = new HashSet<Booking>();
        IsAvailable = true;
    }
}