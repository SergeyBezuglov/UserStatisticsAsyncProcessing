using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PIMS.Application.Common.Interfaces.Services;
using PIMS.Infrastructure.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PIMS.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using PIMS.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Data;
using PIMS.Domain.Common.Authentication.Configuration;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using Serilog.Exceptions;
using System.Reflection;
using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using PIMS.Logger.Services;
using PIMS.Infrastructure.Persistence.Repositories.Common;

namespace PIMS.Infrastructure
{
    /// <summary>
    /// Внедрение зависимостей.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Имя таблицы журнала событий.
        /// </summary>
        private const string EventLogTableName = "EventLog";
        /// <summary>
        /// Добавили инфраструктуру.
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="builder">Строитель.</param>
        /// <returns>Возвращает значение сбора услуг (IServiceCollection).</returns>
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddPersistence(builder.Configuration, out string SelectedProvider,out string SelectedConnectionString);
            services.AddLogging(builder,SelectedProvider, SelectedConnectionString);
            services.AddAuth(builder.Configuration);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            

            return services;
        }
        /// <summary>
        /// Добавили упорства
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="SelectedProvider">Выбранный провайдер.</param>
        /// <param name="SelectedConnectionString">Выбранная строка подключения.</param>
        /// <returns>Возвращает значение сбора услуг (IServiceCollection).</returns>
        private static IServiceCollection AddPersistence(this IServiceCollection services,
            ConfigurationManager configuration,out string SelectedProvider,out string SelectedConnectionString)
        {  
            var dbProviderSettings = new DBProviderSettings();
            configuration.Bind(DBProviderSettings.SectionName, dbProviderSettings);
            configuration.GetSection(DBProviderSettings.SectionName);
            
            services.AddSingleton(Options.Create(dbProviderSettings));
            var InjectedCommandLineArgumentProviderName = configuration.GetValue(DBProviderSettings.ProviderNameArgsForCommandLine, "");
         
            var dbProvider =  dbProviderSettings.ProviderName;
            if (!string.IsNullOrEmpty(InjectedCommandLineArgumentProviderName))
            {
                dbProvider = InjectedCommandLineArgumentProviderName;
            }
            SelectedProvider = dbProvider;
            var connectionString = SelectedConnectionString = configuration.GetConnectionString(dbProvider)!;

            services.AddDbContext<PIMSDbContext>(options => {
                options.EnableDetailedErrors().EnableSensitiveDataLogging();
                options.DatabaseProviderConfiguration(dbProvider!,connectionString!);                
            });         


            services.AddScoped<PublishDomainEventsInterceptor>();
            #region Репозитории
            services.AddScoped<IUserStatisticsRepository, UserStatisticsRepository>();
            #endregion Репозитории
            #region Кэш репозиториев
            //  services.Decorate<IUserRepository, CachedUserRepository>();
            #endregion Кэш репозиториев


            services.AddMemoryCache();
            return services;
        }
        /// <summary>
        /// Базы данных конфигурации провайдера.
        /// </summary>
        /// <param name="options">Варианты.</param>
        /// <param name="provider">Провайдер.</param>
        /// <param name="connectionString">Строка подключения.</param>
        private static void DatabaseProviderConfiguration(this DbContextOptionsBuilder options,string provider,string connectionString)
        {
            if (provider == DBProviderSettings.MSSQLServer)
            {
                options.UseSqlServer(connectionString!,
                           x => x.MigrationsAssembly("PIMS.Migrations.MSQL")
                       );
            }
            if (provider == DBProviderSettings.PostgreSQLServer)
            {
                options.UseNpgsql(
                    connectionString!,
                    x => x.MigrationsAssembly("PIMS.Migrations.PostgreSQL")
                );
            }
        }
        /// <summary>
        /// Добавили логирование.
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="builder">Строитель.</param>
        /// <param name="dbProvider">Провайдер базы данных.</param>
        /// <param name="connectionString">Строка подключения.</param>
        private static void AddLogging(this IServiceCollection services, WebApplicationBuilder builder,string dbProvider,string connectionString)
        {
            
            var assembly = Assembly.GetExecutingAssembly().GetName();
            var LoggerConfiguration= new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .Enrich.WithMachineName()
        .Enrich.WithProperty("Assembly", $"{assembly.Version}"); 
       
            
            if (dbProvider == DBProviderSettings.MSSQLServer)
            {
                var ColumnOptions = new Serilog.Sinks.MSSqlServer.ColumnOptions
                {

                    AdditionalColumns = new Collection<SqlColumn>
                  {
                     new SqlColumn {ColumnName =CustomLoggerExtensions.UserInfoCustomColumnName, PropertyName =CustomLoggerExtensions.UserInfoCustomColumnName, DataType = SqlDbType.NVarChar, DataLength = 500}                
                  }
                };
            
              
                LoggerConfiguration.WriteTo.MSSqlServer(
              connectionString: connectionString,
              sinkOptions: new MSSqlServerSinkOptions
              {

                  TableName = EventLogTableName,
              },

              columnOptions: ColumnOptions);
            }
            if (dbProvider == DBProviderSettings.PostgreSQLServer)
            {
                
                LoggerConfiguration.WriteTo.PostgreSQL(connectionString, EventLogTableName,
                    new Dictionary<string, ColumnWriterBase>
                {
                    {"Id", new RenderedMessageColumnWriter(NpgsqlDbType.Integer) },
                    {"Message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                    {"MessageTemplate", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                    {"Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                    {"TimeStamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                    {"Exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                    {"Properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                    {"UserInfo", new   SinglePropertyColumnWriter("UserInfo")  },
                });
            } 
            Log.Logger = LoggerConfiguration.WriteTo.File(
                new CompactJsonFormatter(),
                Environment.CurrentDirectory + Path.Combine(Path.DirectorySeparatorChar.ToString(), "Logs", "log.json"),
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information).CreateBootstrapLogger();
            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);                
            });
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);
        }
        /// <summary>
        /// Добавили авторизацию.
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <exception cref="Exception"></exception>
        /// <returns>Возвращает значение сбора услуг (IServiceCollection).</returns>
        private static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
         

            var jwtModuleOption = new JwtAuthenticationModuleOption();
            configuration.Bind(JwtAuthenticationModuleOption.SectionName, jwtModuleOption);
            configuration.GetSection(JwtAuthenticationModuleOption.SectionName);

            if (string.IsNullOrEmpty(jwtModuleOption.Name))
            {
                throw new Exception($"Аутентификация с использованием Jwt обязательно должно присутствовать в appSettings.json, но параметр {JwtAuthenticationModuleOption.SectionName} не был обнаружен");
            }
            List<string> AuthSchemes = new List<string>() { JwtAuthenticationModuleOption.SchemeName };
            var activeDirectoryModuleOption = new ActiveDirectoryAuthenticationModuleOption();
            configuration.Bind(ActiveDirectoryAuthenticationModuleOption.SectionName, activeDirectoryModuleOption);
            configuration.GetSection(ActiveDirectoryAuthenticationModuleOption.SectionName);
            var authBuilder = services.AddAuthentication(options =>
            {
                 //options.DefaultScheme = JwtAuthenticationModuleOption.SchemeName;
                 //options.DefaultChallengeScheme = string.IsNullOrEmpty(activeDirectoryModuleOption.Name)?
                
            });
            authBuilder.AddJwtBearer(jwtModuleOption);
            services.AddSingleton(Options.Create(jwtModuleOption));
            if (!string.IsNullOrEmpty(activeDirectoryModuleOption.Name))
            {
                services.AddSingleton(Options.Create(activeDirectoryModuleOption));
                authBuilder.AddNegotiate(activeDirectoryModuleOption);
                if (!string.IsNullOrEmpty(activeDirectoryModuleOption.Name))
                {
                    AuthSchemes.Add(ActiveDirectoryAuthenticationModuleOption.SchemeName);
                }
            }
            
            services.AddAuthorization(AuthSchemes);
            
            
            
            return services;
        }
        /// <summary>
        /// Добавили авторизацию.
        /// </summary>
        /// <param name="services">Услуги.</param>
        /// <param name="AuthSchemes">Схемы аутентификации.</param>
        /// <returns>Возвращает значение сбора услуг (IServiceCollection).</returns>
        private static IServiceCollection AddAuthorization(this IServiceCollection services,List<string> AuthSchemes)
        {
            return services.AddAuthorization(options =>
            {
                //var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(AuthSchemes.ToArray());
                //defaultAuthorizationPolicyBuilder =
                //    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                //options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

        }
        /// <summary>
        /// Добавили носитель jwt.
        /// </summary>
        /// <param name="builder">Строитель.</param>
        /// <param name="jwtModuleOption">Опция модуля jwt.</param>
        /// <returns>Возвращает значение строителя аутентификации (AuthenticationBuilder).</returns>
        private static AuthenticationBuilder AddJwtBearer(this AuthenticationBuilder builder, JwtAuthenticationModuleOption jwtModuleOption)
        {
          return  builder.AddJwtBearer(JwtAuthenticationModuleOption.SchemeName, options =>          
          options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtModuleOption.Settings.Issuer,
                ValidAudience = jwtModuleOption.Settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtModuleOption.Settings.Secret))
            });
        }
        /// <summary>
        /// Добавили переговоры.
        /// </summary>
        /// <param name="builder">Строитель.</param>
        /// <param name="activeDirectoryModuleOption">Опция модуля активного каталога.</param>
        /// <returns>Возвращает значение строителя аутентификации (AuthenticationBuilder).</returns>
        private static AuthenticationBuilder AddNegotiate(this AuthenticationBuilder builder, ActiveDirectoryAuthenticationModuleOption activeDirectoryModuleOption)
        {
            return builder.AddNegotiate(ActiveDirectoryAuthenticationModuleOption.SchemeName, options =>
            {
                options.Events = new Microsoft.AspNetCore.Authentication.Negotiate.NegotiateEvents();
                options.Events!.OnAuthenticated += context =>
                {
                    var d = context.Principal;
                    // Do stuff here
                    return Task.CompletedTask;
                };
                options.Events!.OnChallenge = challange =>
                {
                   
                    return Task.CompletedTask;
                };
                options.Events!.OnAuthenticationFailed = context =>
                {
                    var mes = context.Exception.Message;
                    return Task.CompletedTask;
                };

                options.EnableLdap(settings =>
                {
                    settings.Domain = activeDirectoryModuleOption.Settings.Domain!;
                    if (!string.IsNullOrEmpty(activeDirectoryModuleOption.Settings.Host))
                    {
                        LdapConnection? ldapConnection = null;
                        if (string.IsNullOrEmpty(activeDirectoryModuleOption.Settings.AccessDomainUserName))
                        {
                            ldapConnection = new LdapConnection(activeDirectoryModuleOption.Settings.Host);
                        }
                        else
                        {
                            ldapConnection = new LdapConnection(new LdapDirectoryIdentifier(activeDirectoryModuleOption.Settings.Host),
                                new NetworkCredential(activeDirectoryModuleOption.Settings.AccessDomainUserName,
                                activeDirectoryModuleOption.Settings.AccessDomainUserPassword), AuthType.Basic);
                        }
                        ldapConnection.SessionOptions.ReferralChasing = ReferralChasingOptions.None;
                        settings.LdapConnection = ldapConnection;
                    }
                });

            });
        }
      

    }
}