using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WebCore.Restaurants;
using System.ComponentModel.DataAnnotations;

namespace WebCore.Restaurants.Dto;

[AutoMapFrom(typeof(Restaurant))]
public class RestaurantDto : FullAuditedEntityDto<int>
{
    [Required]
    [StringLength(Restaurant.MaxNameLength)]
    public string Name { get; set; }

    [StringLength(Restaurant.MaxDescriptionLength)]
    public string Description { get; set; }

    [Required]
    [StringLength(Restaurant.MaxAddressLength)]
    public string Address { get; set; }

    [StringLength(Restaurant.MaxPhoneLength)]
    public string Phone { get; set; }

    [StringLength(Restaurant.MaxEmailLength)]
    public string Email { get; set; }

    public int Capacity { get; set; }

    public bool IsActive { get; set; }

    public decimal Rating { get; set; }
}