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
using P12.Models;
using Microsoft.EntityFrameworkCore;

namespace P12
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
				// TODO P12 Task 2-1: Custom Routing Exercise 5
				// Routing example: http://localhost/Shoppers/0L8013 will display order details for order id 0l8013
				// Note: All order id starts with 1 digit, followed by 1 uppercase alphabet ranging from A to L, and followed  by 4 digits
				routes.MapRoute(
				name: "ShoppersByOrderId",
				   template: "Shoppers/{id}",
				   defaults: new { controller = "Order", action = "GetOrder" },
				   constraints: new { id = @"\d[A-L]\d\d\d\d" });

				// TODO P12 Task 2-1: Custom Routing Exercise 4
				// Routing example: http://localhost/Shoppers/MO051 will display product details for product id MO051
				// Note: All product id starts with 2 upper case alphabets and followed by 3 digits
				routes.MapRoute(
				    name: "ShoppersByProductId",
				    template: "Shoppers/{id}",
				    defaults: new { controller = "Product", action = "GetProduct" },
				    constraints: new { id = @"[A-Z][A-Z]\d\d\d" });

				// TODO P12 Task 2-1: Custom Routing Exercise 3
				// Routing example: http://localhost/Shoppers/2018/01/01 will display all orders with order date 2017/01/01
				//Note: oyear must be 4 digits, omonth must be 2 digits, oday must be 2 digits
				//      all variables (oyear, omonth and oday) must be of type integer
				routes.MapRoute(
				    name: "ShoppersByOrderDate",
				    template: "Shoppers/{oyear:int}/{omonth:int}/{oday:int}",
				    defaults: new { controller = "Order", action = "GetOrdersByDate" },
				   constraints: new { oyear = @"\d{4}", omonth = @"\d{2}", oday = @"\d{2}" });

				// TODO P12 Task 2-1: Custom Routing Exercise 2
				// Routing example: http://localhost/Shoppers/Product will display all products
				//  and at the same time allows http://localhost/Shoppers/Product/{action}/{id} to access other 
				//  actions in ProductController
				routes.MapRoute(
				    name: "ShoppersProduct",
				    template: "Shoppers/Product/{action?}/{id?}",
				    defaults: new { controller = "Product", action = "Index" });

				// TODO P12 Task 2-1: Custom Routing Exercise 1
				// Routing example: http://localhost/Shoppers will display all orders
				//  and at the same time allows http://localhost/Shoppers/{action}/{id} to access other 
				//  actions in OrderController
				routes.MapRoute(
				    name: "ShoppersDefault",
				    template: "Shoppers/{action?}/{id?}",
				    defaults: new { controller = "Order", action = "Index" });

				// To be implemented: Add a custom routing to allow direct display of SearchByPatientId action for certain patient id
				// Sample web request http://localhost/JMC255P
				routes.MapRoute(
				    name: "ByPatientId",
				    template: "{id}",
				    defaults: new { controller = "Appointment", action = "SearchByPatientId" },
				    constraints: new { id = @"JMC\d{3}P" });

				// To be implemented: Add a custom routing to allow direct display of SearchByDate action for certain date
				// Sample web request http://localhost/28-01-2016
				routes.MapRoute(
				    name: "ByApptDate",
				    template: "{id}",
				    defaults: new { controller = "Appointment", action = "SearchByDate" },
				    constraints: new { id = @"\d{2}-\d{2}-\d{4}"  });

				routes.MapRoute(name: "default",
						   template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}