using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBusMessage;
using VehicleManagement.BusinessService;
using VehicleManagement.DatabaseService;
using VehicleManagement.Interfaces;
using VehicleManagement.WebAPI.MessegeConsumer.ProcessData;

namespace VehicleManagement.WebAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		readonly string SpecificOriginsAllowed = "_SpecificOriginsAllowed";


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(SpecificOriginsAllowed,
					builder =>
					{
						builder.WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
					});
			});

			ConnectionString connectionStrings = new ConnectionString();
			connectionStrings.DbConnectionString = Configuration.GetSection("ConnectionStrings").Value;
			services.AddDbContext<VehicleDatabase>(
				  o => o.UseSqlServer(
				  Configuration.GetSection("ConnectionStrings").Value));
			services.AddTransient<IVehicleDatabaseService, VehicleDatabaseService>();

			services.AddTransient<IVehicleManagementBusinessService>(provider =>
			new VehicleManagementBusinessService(provider
				.GetRequiredService<IVehicleDatabaseService>(), connectionStrings));

			string ServiceURL = Configuration.GetSection("MessageQueue").GetSection("ServiceURL").Value;
			string CustomerQueue = Configuration.GetSection("MessageQueue").GetSection("CustomerQueue").Value;
			string VehicleStatusQueue = Configuration.GetSection("MessageQueue").GetSection("VehicleStatusQueue").Value;


			services.AddTransient<IMessageConsumer>(provider =>
			new MessageConsumer(ServiceURL, CustomerQueue , new AddCustomer(provider.GetRequiredService<IVehicleDatabaseService>())));

			services.AddTransient<IMessageConsumer>(provider =>
			new MessageConsumer(ServiceURL, VehicleStatusQueue, new ChangeVehicleStatus(provider.GetRequiredService<IVehicleDatabaseService>())));

			services.AddSingleton<IProcessData, AddCustomer>();
			services.AddSingleton<IProcessData, ChangeVehicleStatus>();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseCors(SpecificOriginsAllowed);
			app.UseHttpsRedirection();
			app.UseMvc();
			var bus = app.ApplicationServices.GetService<IMessageConsumer>();
			bus.RegisterOnMessageHandlerAndReceiveMessages();
		}
	}
}
