using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Utility;
using CutomerManagement.BusinessService;
using CutomerManagement.DatabaseService;
using CutomerManagement.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceBusMessage;

namespace CutomerManagement.WebAPI
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
			CorsPolicyBuilder corsBuilder = new CorsPolicyBuilder();
			ConnectionString connectionStrings = new ConnectionString();
			connectionStrings.DbConnectionString = Configuration.GetSection("ConnectionStrings").Value;
			services.AddDbContext<CustomerDatabase>(
				  o => o.UseSqlServer(
				 Configuration.GetSection("ConnectionStrings").Value));
			services.AddTransient<ICustomerDatabaseService, CustomerDatabaseService>();
			services.AddScoped<MessageSender>();
			services.AddTransient<ICustomerManagementBusinessService>(provider =>
			new CustomerManagementBusinessService(provider
				.GetRequiredService<ICustomerDatabaseService>(), connectionStrings,new MessageSender()));
		
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

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
