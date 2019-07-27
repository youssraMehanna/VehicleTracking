using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VehicleManagement.Models.Entities
{
	public class Vehicle
	{
		public int Id { get; set; }
		public string VehicleId { get; set; }
		public string ModelNumber { get; set; }
		public bool IsConnected { get; set; }
		[ForeignKey("CustomerId")]
		public Customer customer { get; set; }
	}
}
