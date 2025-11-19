using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WebCore.EntityFrameworkCore;
using WebCore.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace WebCore.Web.Tests;

[DependsOn(
    typeof(WebCoreWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class WebCoreWebTestModule : AbpModule
{
    public WebCoreWebTestModule(WebCoreEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(WebCoreWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(WebCoreWebMvcModule).Assembly);
    }
}