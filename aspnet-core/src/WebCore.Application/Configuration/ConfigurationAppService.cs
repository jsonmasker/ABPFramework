using Abp.Authorization;
using Abp.Runtime.Session;
using WebCore.Configuration.Dto;
using System.Threading.Tasks;

namespace WebCore.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : WebCoreAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
