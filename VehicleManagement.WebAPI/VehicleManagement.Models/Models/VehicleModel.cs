using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleManagement.Models.Models
{
	public class VehicleModel
	{
		public int Id { get; set; }
		public string VehicleId { get; set; }
		public string ModelNumber { get; set; }
		public bool IsConnected { get; set; }
		public int CustomerId { get; set; }
		public string CustomerName { get; set; }
	}
}
