using Abp.Zero.EntityFrameworkCore;
using WebCore.Authorization.Roles;
using WebCore.Authorization.Users;
using WebCore.MultiTenancy;
using WebCore.Restaurants;
using Microsoft.EntityFrameworkCore;

namespace WebCore.EntityFrameworkCore;

public class WebCoreDbContext : AbpZeroDbContext<Tenant, Role, User, WebCoreDbContext>
{
    /* Define a DbSet for each entity of the application */

    // Restaurant booking entities
    public virtual DbSet<Restaurant> Restaurants { get; set; }
    public virtual DbSet<Table> Tables { get; set; }
    public virtual DbSet<Booking> Bookings { get; set; }

    public WebCoreDbContext(DbContextOptions<WebCoreDbContext> options)
        : base(options)
    {
    }
}
