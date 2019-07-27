using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusMessage.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessage
{
	public class MessageSender
	{
		const string ServiceBusConnectionString = "Endpoint=sb://vtservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=XbcvqhgK6qSYTb7UtB1g80yKr9fOQKjyJhbgKumaJ5g=";
		const string QueueName = "sbqueue";
		static IQueueClient queueClient;
		public MessageSender()
		{
			queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
		}
		public async Task SendMessage(Customer customer)
		{
			string data = JsonConvert.SerializeObject(customer);
			Message message = new Message(Encoding.UTF8.GetBytes(data));
            await queueClient.SendAsync(message);
		}

	}	
}