using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PingService.Timer;
using ServiceBusMessage.ServiceBusMessage.Interface;
using ServiceBusMessage.ServiceBusMessage.Sender;

namespace PingService
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddTransient<TimedHostedService>(s => new TimedHostedService(s.GetRequiredService<IVehicleStatusSender>()));
			services.AddHostedService<TimedHostedService>();

			string ServiceURL = Configuration.GetSection("MessageQueue").GetSection("ServiceURL").Value;
			string QueueName = Configuration.GetSection("MessageQueue").GetSection("QueueName").Value;

			services.AddTransient<IVehicleStatusSender, VehicleStatusSender>();
			services.AddTransient<IVehicleStatusSender>(provider =>
					new VehicleStatusSender(ServiceURL, QueueName));
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

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
