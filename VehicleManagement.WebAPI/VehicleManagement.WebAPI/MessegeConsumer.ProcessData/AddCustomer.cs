using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusMessage;
using ServiceBusMessage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Interfaces;

namespace VehicleManagement.WebAPI.MessegeConsumer.ProcessData
{
	public class AddCustomer : IProcessData
	{
		IVehicleDatabaseService _ivehicleDatabaseService;
		public AddCustomer(IVehicleDatabaseService ivehicleDatabaseService)
		{
			_ivehicleDatabaseService = ivehicleDatabaseService;
		}
		public async Task ProcessAsync(Message retriveMessage)
		{
			try
			{
				var customer = JsonConvert.DeserializeObject<Customer>(Encoding.UTF8.GetString(retriveMessage.Body));
				await _ivehicleDatabaseService.AddCustomer(new Models.Entities.Customer()
				{
					Name = customer.Name,
					Id = customer.Id
				});
			}
			catch (Exception ex)
			{
			}
		}
	}
}
