using Microsoft.Azure.ServiceBus;
using ServiceBusMessage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessage
{
	public interface IProcessData
	{
		Task ProcessAsync(Message retriveMessage);
	}
}
