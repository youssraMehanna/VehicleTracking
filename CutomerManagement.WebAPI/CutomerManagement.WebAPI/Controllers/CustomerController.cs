using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutomerManagement.Interfaces;
using CutomerManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusMessage;

namespace CutomerManagement.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerManagementBusinessService _customerBusinessService;
	


		public CustomerController(ICustomerManagementBusinessService customerBusinessService)
		{
			_customerBusinessService = customerBusinessService;
			//_serviceBus = serviceBus;
		}
		[HttpPost]
		public async Task<IActionResult> Register([FromBody] CustomerModel customerModel)
		{
			await _customerBusinessService.RegisterCustomerAsync(customerModel);
		
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetCustomer()
		{
			await _customerBusinessService.GetCustomers();

			return Ok();
		}

	}
	
}