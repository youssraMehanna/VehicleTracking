using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleManagement.Interfaces;
using VehicleManagement.Models.Entities;
using VehicleManagement.Models.Models;
using Xunit;

namespace VehicleManagement.UnitTest
{
	public class VehicleManagementBusinessServiceFake : IVehicleDatabaseService
	{
		public Task AddCustomer(Customer customer)
		{
			throw new NotImplementedException();
		}

		public void UpdateVehicleStatus(int vehicleId, bool Status)
		{

		}

		async Task<List<Vehicle>> IVehicleDatabaseService.GetVehiclesWithCustomer()
		{
			List<Vehicle> vehicles = new List<Vehicle>() {
				new Vehicle {Id=1 , ModelNumber="Modl1" ,VehicleId="4444", IsConnected=true },
				new Vehicle {Id=2 , ModelNumber="Modl2" ,VehicleId="5555", IsConnected=false }

			};
			return vehicles;
		}

	}
}
