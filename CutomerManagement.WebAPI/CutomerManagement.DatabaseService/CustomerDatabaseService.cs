using CutomerManagement.Interfaces;
using CutomerManagement.Models;
using Microsoft.EntityFrameworkCore;
using ServiceBusMessage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CutomerManagement.DatabaseService
{
	public class CustomerDatabaseService : ICustomerDatabaseService
	{
		private CustomerDatabase _context;
		public CustomerDatabaseService()
		{
			_context = new CustomerDatabase();
		}

		public CustomerDatabase dbConnection
		{
			get { return _context; }
		}
		public async Task<List<Customer>> GetCustomers()
		{
			List<Customer> customers = await dbConnection.Customers.ToListAsync();
			return customers;
		}

		public async Task<Customer> RegisterCustomer(Customer customer)
		{
			await dbConnection.Customers.AddAsync(customer);
			dbConnection.SaveChangesAsync();
			return customer;
		}
	}
}
