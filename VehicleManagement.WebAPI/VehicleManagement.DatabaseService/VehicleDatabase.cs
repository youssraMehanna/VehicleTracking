using Common.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using VehicleManagement.Models.Entities;

namespace VehicleManagement.DatabaseService
{
	public class VehicleDatabase : DbContext
	{
		public VehicleDatabase(DbContextOptions<VehicleDatabase> options) : base(options)
		{
		}
		public VehicleDatabase()
		{
		}
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionStrings = CommonFunction.GetConnectionStrings();
			string databaseConnectionString = connectionStrings.DbConnectionString;
			Console.WriteLine("Connecting to Database = " + databaseConnectionString);

			optionsBuilder.UseSqlServer(databaseConnectionString);
		}
	}
}
