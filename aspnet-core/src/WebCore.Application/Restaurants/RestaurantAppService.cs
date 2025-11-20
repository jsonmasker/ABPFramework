using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using WebCore.Authorization;
using WebCore.Restaurants.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace WebCore.Restaurants;

[AbpAuthorize(PermissionNames.Pages_Restaurants)]
public class RestaurantAppService : AsyncCrudAppService<Restaurant, RestaurantDto, int, GetRestaurantsInput, CreateRestaurantDto, RestaurantDto>, IRestaurantAppService
{
    public RestaurantAppService(IRepository<Restaurant, int> repository)
        : base(repository)
    {
        CreatePermissionName = PermissionNames.Pages_Restaurants_Create;
        UpdatePermissionName = PermissionNames.Pages_Restaurants_Edit;
        DeletePermissionName = PermissionNames.Pages_Restaurants_Delete;
    }

    [AbpAllowAnonymous]
    public async Task<ListResultDto<RestaurantDto>> GetAllActiveRestaurantsAsync()
    {
        var restaurants = await Repository.GetAllListAsync(r => r.IsActive);
        return new ListResultDto<RestaurantDto>(
            ObjectMapper.Map<List<RestaurantDto>>(restaurants)
        );
    }

    protected override IQueryable<Restaurant> CreateFilteredQuery(GetRestaurantsInput input)
    {
        return Repository.GetAll()
            .WhereIf(!input.Filter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Filter) || x.Address.Contains(input.Filter))
            .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
    }

    protected override IQueryable<Restaurant> ApplySorting(IQueryable<Restaurant> query, GetRestaurantsInput input)
    {
        return query.OrderBy(input.Sorting ?? "name asc");
    }
}