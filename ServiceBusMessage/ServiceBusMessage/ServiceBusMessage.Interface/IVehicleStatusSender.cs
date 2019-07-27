using MessageQueue.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusMessage.ServiceBusMessage.Interface
{
	public interface IVehicleStatusSender
	{
		Task SendMessage(VehicleStatus status);
	}
}
