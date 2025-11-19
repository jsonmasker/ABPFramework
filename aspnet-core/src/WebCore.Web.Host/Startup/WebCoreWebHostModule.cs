using Abp.Modules;
using Abp.Reflection.Extensions;
using WebCore.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WebCore.Web.Host.Startup
{
    [DependsOn(
       typeof(WebCoreWebCoreModule))]
    public class WebCoreWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public WebCoreWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WebCoreWebHostModule).GetAssembly());
        }
    }
}
