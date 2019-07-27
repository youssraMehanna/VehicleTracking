using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBusMessage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusMessage
{
	public class MessageConsumer: IMessageConsumer
	{
		static IQueueClient queueClient;
		private readonly IProcessData _processData;

		public MessageConsumer(string ServiceBusConnectionString, string QueueName,IProcessData processData)
		{
			_processData = processData;
			queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
		}
		public void RegisterOnMessageHandlerAndReceiveMessages()
		{
			var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
			{
				MaxConcurrentCalls = 1,
				AutoComplete = false
			};
			queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
		}

		private async Task ProcessMessagesAsync(Message message, CancellationToken token)
		{
			//await ProccessData(message);
			//var myPayload = JsonConvert.DeserializeObject<Customer>(Encoding.UTF8.GetString(message.Body));
			//await _processData.Process(myPayload);
			await _processData.ProcessAsync(message);
			await queueClient.CompleteAsync(message.SystemProperties.LockToken);
		}
		
		private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
		{
			return Task.CompletedTask;
		}

		public async Task CloseQueueAsync()
		{
			await queueClient.CloseAsync();
		}

	}
}
