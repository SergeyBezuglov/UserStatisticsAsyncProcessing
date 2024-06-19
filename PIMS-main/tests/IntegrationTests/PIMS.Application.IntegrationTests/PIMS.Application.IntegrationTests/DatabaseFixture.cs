using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Respawn;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Respawn.Graph;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using PIMS.Infrastructure.Persistence.DbContexts;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.FileProviders;
using PIMS.Domain.Common.Models.Base;

namespace PIMS.Application.IntegrationTests
{
	public class DatabaseFixture : IDisposable
	{
		private static IConfigurationRoot _configuration;
		private static IServiceScopeFactory _scopeFactory;
		private static Respawner _checkpoint;


		public DatabaseFixture()
		{
			var ConfigBuilder = new ConfigurationBuilder()
			  .SetBasePath(Directory.GetCurrentDirectory())
			  .AddJsonFile("appsettingsTest.json", true, true)
			  .AddEnvironmentVariables();


			var webApplicationFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
			{
				builder.UseConfiguration(ConfigBuilder.Build());
				builder.ConfigureServices(services =>
				{
					services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
				  w.EnvironmentName == "Development" &&
				  w.ApplicationName == "PIMS.Web"));
					services.AddLogging();
				 
				});
				builder.ConfigureAppConfiguration((context, builder) =>
				{
					builder.SetBasePath(context.HostingEnvironment.ContentRootPath);
					builder.AddJsonFile("appsettingsTest.json", true, true);
					builder.AddEnvironmentVariables();
 
					_configuration = builder.Build();
				});
			});

			_scopeFactory = webApplicationFactory.Services.GetService<IServiceScopeFactory>();

			ConfigureDatabaseReset().GetAwaiter().GetResult();

		}
		private async Task ConfigureDatabaseReset()
		{
			EnsureDatabase();
			var ConnStr = _configuration.GetConnectionString("MSSQLServer");
			if (ConnStr is not null)
			{
				_checkpoint = await Respawner.CreateAsync(ConnStr, new RespawnerOptions
				{
					TablesToIgnore = new Table[]
				  {
					"__EFMigrationsHistory"
				  }
				});
			}
		}

		private void EnsureDatabase()
		{
			using var scope = _scopeFactory.CreateScope();
			var context = scope.ServiceProvider.GetService<PIMSDbContext>();
			if (context is not null)
			{
				context.Database.Migrate();
			}
		}

		public async Task ResetState()
		{
			await _checkpoint.ResetAsync(_configuration.GetConnectionString("MSSQLServer"));

		}

		public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
		{
			using var scope = _scopeFactory.CreateScope();
			var mediator = scope.ServiceProvider.GetService<IMediator>();
			return await mediator.Send(request);
		}

		public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
		{
			using var scope = _scopeFactory.CreateScope();
			var context = scope.ServiceProvider.GetService<PIMSDbContext>();
			if (context is not null)
			{
				context.Add(entity);
			}
			await context.SaveChangesAsync();
		}

		public async Task<TEntity> FindAsync<TEntity>(AggregateRootId<Guid> id)
			where TEntity : class
		{
			using var scope = _scopeFactory.CreateScope();

			var context = scope.ServiceProvider.GetService<PIMSDbContext>();

			return await context.FindAsync<TEntity>(id);
		}

		public void Dispose()
		{
			// Code to run after all tests
		}
	}
}