using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleManagement.Models.Models;

namespace VehicleManagement.Interfaces
{
	public interface IVehicleManagementBusinessService
	{
		Task<List<VehicleModel>> GetVehiclesWithCustomer();
	}
}
