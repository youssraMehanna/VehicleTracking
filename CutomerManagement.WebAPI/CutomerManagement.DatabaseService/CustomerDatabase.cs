using Common.Utility;
using CutomerManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CutomerManagement.DatabaseService
{
	public class CustomerDatabase:DbContext
	{
		public CustomerDatabase()
		{
		}
		public CustomerDatabase(DbContextOptions<CustomerDatabase> options) : base(options)
		{
		}
		public DbSet<Customer> Customers { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionStrings = CommonFunction.GetConnectionStrings();
			string databaseConnectionString = connectionStrings.DbConnectionString;
			Console.WriteLine("Connecting to Database = " + databaseConnectionString);

			optionsBuilder.UseSqlServer(databaseConnectionString);
		}
	}
}
