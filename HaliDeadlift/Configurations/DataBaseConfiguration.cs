using HaliDeadlift.Infrastructure;
using HalilDeadlift.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HaliDeadlift.Configurations
{
    public static class DataBaseConfiguration
    {
        public static async Task ConfigureDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DbInitializer");

            try
            {
                // migrate database changes on application startup
                await dbContext.Database.MigrateAsync();

                if (dbContext.Exercises.Any()) return;

                // initialize exercises, muscle groups and equipments data
                await DbInitializer.InitializeDatabaseData(dbContext, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the database: {ex.Message}");

                throw;
            }
        }
    }
}
