using CutomerManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CutomerManagement.Interfaces
{
	public interface ICustomerManagementBusinessService
	{
		Task<List<CustomerModel>> GetCustomers();
		Task RegisterCustomerAsync(CustomerModel customerModel);
	}
}
