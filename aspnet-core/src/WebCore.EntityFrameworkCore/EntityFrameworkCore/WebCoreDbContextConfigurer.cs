using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace WebCore.EntityFrameworkCore;

public static class WebCoreDbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<WebCoreDbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<WebCoreDbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
