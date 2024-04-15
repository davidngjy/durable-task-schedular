using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DurableTaskSchedular.Web;

internal static class DatabaseMigration
{
    public static void RunMigration(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate();
    }
}
