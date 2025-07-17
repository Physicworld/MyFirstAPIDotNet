using Microsoft.EntityFrameworkCore;

namespace testwithnet8.Data
{
    public class DatabaseMigrator(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var scope = serviceProvider.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await db.Database.MigrateAsync(cancellationToken);
        }
    }
}