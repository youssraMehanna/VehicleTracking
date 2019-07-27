using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Interfaces;
using VehicleManagement.Models.Entities;
using VehicleManagement.Models.Models;

namespace VehicleManagement.DatabaseService
{
	public class VehicleDatabaseService : IVehicleDatabaseService
	{
		private VehicleDatabase _context;

		public VehicleDatabaseService()
		{
			_context = new VehicleDatabase();
		}
		public VehicleDatabase dbConnection
		{
			get { return _context; }
		}

		public async Task AddCustomer(Customer customer)
		{
			await dbConnection.Customers.AddAsync(customer);
			await dbConnection.SaveChangesAsync();

		}

		public async Task<List<Vehicle>> GetVehiclesWithCustomer()
		{
			List<Vehicle> vehicles = await dbConnection.Vehicles.ToListAsync();
			return vehicles;
		}

		public async void UpdateVehicleStatus(int vehicleId, bool Status)
		{

			var vehicle = dbConnection.Vehicles.SingleOrDefault(v => v.Id == vehicleId);
			if (vehicle != null)
			{
				vehicle.IsConnected = Status;
				await dbConnection.SaveChangesAsync();
			}

		}

	
	}
}
