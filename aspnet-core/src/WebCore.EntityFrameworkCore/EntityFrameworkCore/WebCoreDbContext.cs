using Abp.Zero.EntityFrameworkCore;
using WebCore.Authorization.Roles;
using WebCore.Authorization.Users;
using WebCore.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace WebCore.EntityFrameworkCore;

public class WebCoreDbContext : AbpZeroDbContext<Tenant, Role, User, WebCoreDbContext>
{
    /* Define a DbSet for each entity of the application */

    public WebCoreDbContext(DbContextOptions<WebCoreDbContext> options)
        : base(options)
    {
    }
}
