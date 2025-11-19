using Abp.Application.Services;
using WebCore.MultiTenancy.Dto;

namespace WebCore.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

