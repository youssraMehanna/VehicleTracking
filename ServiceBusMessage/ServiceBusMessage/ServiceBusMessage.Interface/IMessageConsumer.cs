using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessage
{
	public interface IMessageConsumer
	{
		void RegisterOnMessageHandlerAndReceiveMessages();
		Task CloseQueueAsync();
	}
}
