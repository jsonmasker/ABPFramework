using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WebCore.Restaurants;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Restaurants.Dto;

[AutoMapFrom(typeof(Table))]
public class TableDto : FullAuditedEntityDto<int>
{
    [Required]
    [StringLength(Table.MaxNameLength)]
    public string Name { get; set; }

    public int Capacity { get; set; }

    public bool IsAvailable { get; set; }

    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; }
}