using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WebCore.Authorization;

namespace WebCore;

[DependsOn(
    typeof(WebCoreCoreModule),
    typeof(AbpAutoMapperModule))]
public class WebCoreApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<WebCoreAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(WebCoreApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
