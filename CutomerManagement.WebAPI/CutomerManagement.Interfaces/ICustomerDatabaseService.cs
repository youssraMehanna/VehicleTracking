using CutomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CutomerManagement.Interfaces
{
	public interface ICustomerDatabaseService
	{
		Task<Customer> RegisterCustomer(Customer customer);
		Task<List<Customer>> GetCustomers();

	}
}
