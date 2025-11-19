using WebCore.Configuration.Dto;
using System.Threading.Tasks;

namespace WebCore.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
