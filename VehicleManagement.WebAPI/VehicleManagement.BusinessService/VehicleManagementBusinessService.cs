using Common.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleManagement.Interfaces;
using VehicleManagement.Models.Entities;
using VehicleManagement.Models.Models;

namespace VehicleManagement.BusinessService
{
	public class VehicleManagementBusinessService : IVehicleManagementBusinessService
	{
		private readonly ConnectionString _connectionStrings;
		IVehicleDatabaseService _ivehicleDatabaseService;
		public VehicleManagementBusinessService(IVehicleDatabaseService ivehicleDatabaseService, ConnectionString connectionString)
		{
			_ivehicleDatabaseService = ivehicleDatabaseService;
		}

		public async Task<List<VehicleModel>> GetVehiclesWithCustomer()
		{
			var vehicles =await _ivehicleDatabaseService.GetVehiclesWithCustomer();
			List<VehicleModel> vehicleModels = new List<VehicleModel>() ;
			foreach (var vehicle in vehicles)
			{
				vehicleModels.Add(new VehicleModel()
				{
					Id = vehicle.Id,
					VehicleId = vehicle.VehicleId,
					ModelNumber = vehicle.ModelNumber,
					IsConnected=vehicle.IsConnected,
					CustomerName = vehicle.customer != null ? vehicle.customer.Name : "",
					CustomerId = vehicle.customer != null ? vehicle.customer.Id : 0
				});
			}
			return vehicleModels;
		}
	}
}
