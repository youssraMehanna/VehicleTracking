using MessageQueue.Models;
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
	public class ChangeVehicleStatus : IProcessData
	{
		IVehicleDatabaseService _ivehicleDatabaseService;
		public ChangeVehicleStatus(IVehicleDatabaseService ivehicleDatabaseService)
		{
			_ivehicleDatabaseService = ivehicleDatabaseService;
		}
		public async Task ProcessAsync(Message retriveMessage)
		{
			try
			{
				var vehicle = JsonConvert.DeserializeObject<VehicleStatus>(Encoding.UTF8.GetString(retriveMessage.Body));
				 _ivehicleDatabaseService.UpdateVehicleStatus(vehicle.Id,vehicle.IsConnected);
			}
			catch (Exception ex)
			{
			}
		}
	}
}
