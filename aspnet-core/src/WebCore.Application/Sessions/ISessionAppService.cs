using Abp.Application.Services;
using WebCore.Sessions.Dto;
using System.Threading.Tasks;

namespace WebCore.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
