using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace WebCore.Controllers
{
    public abstract class WebCoreControllerBase : AbpController
    {
        protected WebCoreControllerBase()
        {
            LocalizationSourceName = WebCoreConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
