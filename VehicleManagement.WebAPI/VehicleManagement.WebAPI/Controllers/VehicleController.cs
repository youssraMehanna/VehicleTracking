using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.Interfaces;
using VehicleManagement.Models.Models;

namespace VehicleManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
		private readonly IVehicleManagementBusinessService _vehicleBusinessService;
		public VehicleController(IVehicleManagementBusinessService vehicleBusinessService)
		{
			_vehicleBusinessService = vehicleBusinessService;
		}
		[HttpGet]
		public async Task<List<VehicleModel>> GetVehiclesWithCustomer()
		{
			return await _vehicleBusinessService.GetVehiclesWithCustomer();
		}
	}
}