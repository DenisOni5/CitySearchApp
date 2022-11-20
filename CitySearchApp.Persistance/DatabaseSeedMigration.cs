using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CitySearchApp.Persistance
{
    public static class DatabaseSeedMigration
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            using (CitySearchAppDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<CitySearchAppDbContext>())
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
