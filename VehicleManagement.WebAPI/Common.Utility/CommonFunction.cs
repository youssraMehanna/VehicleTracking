using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Common.Utility
{
	public class CommonFunction
	{
		public static ConnectionString GetConnectionStrings()
		{
			string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			string jsonFile = $"appsettings.json";

			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile(jsonFile, optional: false, reloadOnChange: true)
			.AddEnvironmentVariables();

			IConfigurationRoot configuration = builder.Build();

			ConnectionString connectionString = new ConnectionString();
			connectionString.DbConnectionString = configuration.GetSection("ConnectionStrings").Value;

			return connectionString;
		}
	}
}
