using AuthAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AuthAPI.HealthChecks
{
    public class DatabaseHealthChecks : IHealthCheck
    {
        private readonly IDbContextFactory<DataContext> dbContextFactory;

        public DatabaseHealthChecks(IDbContextFactory<DataContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var result = await isOpen();
            return result ? 
                HealthCheckResult.Healthy("Database connection is OK") : 
                HealthCheckResult.Unhealthy("Database connection is ERROR");
        }

        private async Task<bool> isOpen()
        {
            using var context = await dbContextFactory.CreateDbContextAsync();

            return context.Users.Any();
        }
    }
}
