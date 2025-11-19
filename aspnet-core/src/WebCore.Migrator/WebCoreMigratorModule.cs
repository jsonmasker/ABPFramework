using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WebCore.Configuration;
using WebCore.EntityFrameworkCore;
using WebCore.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace WebCore.Migrator;

[DependsOn(typeof(WebCoreEntityFrameworkModule))]
public class WebCoreMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public WebCoreMigratorModule(WebCoreEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(WebCoreMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            WebCoreConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(WebCoreMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
