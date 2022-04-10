namespace NationalLandmarks.Server.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;

    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<NationalLandmarksDbContext>();

            dbContext.Database.Migrate();
        } 
    }
}
