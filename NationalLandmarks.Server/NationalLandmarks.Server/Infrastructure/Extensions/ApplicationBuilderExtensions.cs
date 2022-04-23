namespace NationalLandmarks.Server.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using NationalLandmarks.Server.Data;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Landmark API v1");
                    options.RoutePrefix = String.Empty;
                });
        }
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            var dbContext = services.ServiceProvider.GetService<NationalLandmarksDbContext>();

            dbContext.Database.Migrate();
        } 
    }
}
