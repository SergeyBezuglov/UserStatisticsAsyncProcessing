using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.IntegrationTests
{
	public class TestServerFactory : WebApplicationFactory<Program>
	{
		public IConfigurationRoot configuration;

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{

				services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
			  w.EnvironmentName == "Development" &&
			  w.ApplicationName == "PIMS.Web"));
				services.AddLogging();
			});
			builder.ConfigureAppConfiguration((context, builder) =>
			{
				builder.SetFileProvider(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
				builder.SetBasePath(Directory.GetCurrentDirectory());
				builder.AddJsonFile("appsettingsTest.json", true, true);
				builder.AddEnvironmentVariables();

				configuration = builder.Build();
			});
			base.ConfigureWebHost(builder);
		}
	}
}
