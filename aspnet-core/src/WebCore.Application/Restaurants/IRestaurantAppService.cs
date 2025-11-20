using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WebCore.Restaurants.Dto;
using System.Threading.Tasks;

namespace WebCore.Restaurants;

public interface IRestaurantAppService : IApplicationService
{
    Task<PagedResultDto<RestaurantDto>> GetAllAsync(GetRestaurantsInput input);
    Task<RestaurantDto> GetAsync(EntityDto<int> input);
    Task<RestaurantDto> CreateAsync(CreateRestaurantDto input);
    Task<RestaurantDto> UpdateAsync(RestaurantDto input);
    Task DeleteAsync(EntityDto<int> input);
    Task<ListResultDto<RestaurantDto>> GetAllActiveRestaurantsAsync();
}