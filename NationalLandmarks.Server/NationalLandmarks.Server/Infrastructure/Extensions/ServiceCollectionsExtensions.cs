namespace NationalLandmarks.Server.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Area;
    using NationalLandmarks.Server.Features.Identity;
    using NationalLandmarks.Server.Features.Landmark;
    using NationalLandmarks.Server.Features.Notification;
    using NationalLandmarks.Server.Features.Place;
    using NationalLandmarks.Server.Features.Visit;
    using NationalLandmarks.Server.Infrastructure.Filters;
    using NationalLandmarks.Server.Infrastructure.Services;
    using System.Reflection;
    using System.Text;

    public static class ServiceCollectionsExtensions
    {
        public static AppSettings GetApplicationSettings(
            this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppSettings>();
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services, IConfiguration configuration)
        {
            // register as a scoped service in the application service provider (a.k.a. the dependency injection container)
            return services
                .AddDbContext<NationalLandmarksDbContext>(options => 
                    options.UseSqlServer(configuration.GetDefaultConnectionString()));
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<NationalLandmarksDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddTransient<IIdentityService, IdentityService>()
                           .AddScoped<ICurrentUserService, CurrentUserService>()
                           .AddTransient<ILandmarkService, LandmarkService>()
                           .AddTransient<IPlaceService, PlaceService>()
                           .AddTransient<IVisitService, VisitService>()
                           .AddTransient<IAreaService, AreaService>()
                           .AddTransient<IEmailService, EmailService>();
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Version = "v1", 
                    Title = "Landmark API", 
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    }
                });
                // Configure Swagger to use the XML file that's generated
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Build an XML file name matching that of the web API project
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename)); // AppContext.BaseDirectory property is used to construct a path to the XML file
            });
        }

        public static void AddApiControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ModelOrNotFoundActionFilter>();
            });
        }
    }
}
