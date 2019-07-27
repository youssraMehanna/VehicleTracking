using Common.Utility;
using CutomerManagement.Interfaces;
using CutomerManagement.Models;
using ServiceBusMessage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CutomerManagement.BusinessService
{
	public class CustomerManagementBusinessService : ICustomerManagementBusinessService
	{
		private readonly ConnectionString _connectionStrings;
		private readonly MessageSender _serviceBus;
		ICustomerDatabaseService _icustomerDatabaseService;
		public CustomerManagementBusinessService(ICustomerDatabaseService icustomerDatabaseService,ConnectionString connectionString, MessageSender serviceBus)
		{
			_icustomerDatabaseService = icustomerDatabaseService;
			_serviceBus = serviceBus;


		}

		public async Task RegisterCustomerAsync(CustomerModel  customerModel)
		{
			var customer = new Customer()
			{
				Address = customerModel.Address,
				Id = customerModel.Id,
				Name = customerModel.Name
			};

			Customer insertedCustomer =await  _icustomerDatabaseService.RegisterCustomer(customer);
			await _serviceBus.SendMessage(new ServiceBusMessage.Models.Customer()
			{
				Id = insertedCustomer.Id,
				Name = insertedCustomer.Name
			});
		}

		public async Task<List<CustomerModel>> GetCustomers()
		{
			List<CustomerModel> LSTmODEL = new List<CustomerModel>();
			List<Customer> list = await _icustomerDatabaseService.GetCustomers();
			foreach (var CUSTOMER in list)
			{
				LSTmODEL.Add(new CustomerModel()
				{
					Address = CUSTOMER.Address,
					Id = CUSTOMER.Id,
					Name = CUSTOMER.Name
				});
			}
			return LSTmODEL;
		}
	}
}
