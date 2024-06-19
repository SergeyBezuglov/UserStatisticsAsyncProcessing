using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PIMS.Web.Common.Errors;

using PIMS.Web.Extensions;
using PIMS.Web.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;
using PIMS.Web.Middleware;
using Vite.AspNetCore.Extensions;
using System.Text.Json.Serialization;
using PIMS.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Infrastructure.Persistence.Repositories.Common;
using PIMS.Infrastructure.Services;
using PIMS.Domain.Common;

namespace PIMS.Web
{
    /// <summary>
    /// Внедрение зависимостей.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services
                .AddEndpointsApiExplorer()
                .AddApiVersioningExtension()
                .AddVersionedApiExplorerExtension()
                .AddSwaggerGenExtension();

            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;

                // serialize enums as strings in api responses (e.g. Role)
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddSpaStaticFiles(options =>
            {
                options.RootPath = "client/dist";
            });

            services.AddViteServices();
            services.AddCors();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<ProblemDetailsFactory, PIMSProblemDetailsFactory>();
            services.AddSingleton<ValidateAuthentication>();

            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true
            );

            // Регистрация репозитория
            services.AddScoped<IUserStatisticsRepository, UserStatisticsRepository>();

            // Регистрация конфигурационных настроек
            services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // Регистрация фонового сервиса
            services.AddHostedService<UserStatisticsProcessingService>();

            return services;
        }

        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PIMSDbContext>();
                dbContext.Database.Migrate();
            }
        }

        public static void ConfigureSPA(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseViteDevMiddleware();
            }
            else
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "client";
                });
            }
        }
    }
}