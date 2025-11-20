using Abp.Application.Services.Dto;

namespace WebCore.Restaurants.Dto;

public class GetRestaurantsInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
    public bool? IsActive { get; set; }
}