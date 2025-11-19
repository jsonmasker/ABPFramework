using Abp.Application.Services;
using WebCore.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace WebCore.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
