using NationalLandmarks.Server.Infrastructure.Extensions;
using NationalLandmarks.Server.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDatabase(builder.Configuration)
    .AddIdentity()
    .AddJwtAuthentication(builder.Services.GetApplicationSettings(builder.Configuration))
    .AddApplicationServices()
    .AddSwagger()
    .AddApiControllers();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}

/*app.UseHttpsRedirection();
app.UseStaticFiles();*/

app
    .UseSwaggerUI()
    .UseRouting()
    .UseCors(options => options
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyHeader())
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints => endpoints.MapControllers())
    .ApplyMigrations();

app.Run();
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();*/