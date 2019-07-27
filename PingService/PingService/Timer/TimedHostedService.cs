using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceBusMessage.ServiceBusMessage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace PingService.Timer
{
	public class TimedHostedService : IHostedService, IDisposable
	{
		private System.Threading.Timer _timer;
		IVehicleStatusSender _iVehicleStatusSender;
		bool isConnected = true;
		public TimedHostedService(IVehicleStatusSender iVehicleStatusSender)
		{
			_iVehicleStatusSender = iVehicleStatusSender;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			_timer = new System.Threading.Timer(DoWork, null, TimeSpan.Zero,
				TimeSpan.FromMinutes(3));

			return Task.CompletedTask;
		}

		private void DoWork(object state)
		{
			isConnected = !isConnected;
			_iVehicleStatusSender.SendMessage(new MessageQueue.Models.VehicleStatus() { Id=1,IsConnected= isConnected });
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}

