using MessageQueue.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusMessage.ServiceBusMessage.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessage.ServiceBusMessage.Sender
{
	public class VehicleStatusSender : IVehicleStatusSender
	{
		static IQueueClient queueClient;

		public VehicleStatusSender(string ServiceBusConnectionString, string QueueName)
		{
			queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
		}
		public async Task SendMessage(VehicleStatus status)
		{
			string data = JsonConvert.SerializeObject(status);
			Message message = new Message(Encoding.UTF8.GetBytes(data));
			await queueClient.SendAsync(message);
		}
	}
	
}
