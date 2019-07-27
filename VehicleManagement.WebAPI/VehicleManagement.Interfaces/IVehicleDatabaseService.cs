using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Models.Entities;

namespace VehicleManagement.Interfaces
{
	public interface IVehicleDatabaseService
	{
		Task<List<Vehicle>> GetVehiclesWithCustomer();
		Task AddCustomer(Customer customer);
		void UpdateVehicleStatus(int vehicleId, bool Status);
	}
}
