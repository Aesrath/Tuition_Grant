using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using P09.Models;
using Microsoft.EntityFrameworkCore;

namespace P09
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; }
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add …
		// For more information on how to configure your application, …
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AppDbContext>(
						options =>
							options
							   .UseSqlServer(
									Configuration.GetConnectionString("dbstr")));
			services.AddDistributedMemoryCache();
			services.AddSession();
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure …
		public void Configure(IApplicationBuilder app, IHostingEnvironment env,
							  ILoggerFactory loggerFactory)
		{
			app.UseDeveloperExceptionPage();
			app.UseDefaultFiles();
			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				AuthenticationScheme = "UserSecurity",
				LoginPath = new PathString("/Account/Login/"),
				AccessDeniedPath = new PathString("/Account/Forbidden/"),
				AutomaticAuthenticate = true,
				AutomaticChallenge = true
			});
			app.UseStaticFiles();
			app.UseSession();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					   name: "default",
					   template: "{controller=Home}/{action=Index}/{id?}");
			});

		}
	}
}