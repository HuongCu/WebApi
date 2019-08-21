using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simple.Api.Common;
using Simple.Api.Model;
using Simple.Api.Services;

namespace Simple.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomersController : ControllerBase
	{
		private ICustomerService _customerService;
		public CustomersController(ICustomerService customerService)
		{

			_customerService = customerService;
		}
		/// <summary>
		/// Search customers by firstName or lastName
		/// </summary>
		/// <param name="searchString"></param>
		/// <returns></returns>
		// GET api/customers
		[HttpGet]
		public async Task<ActionResult> Get([FromQuery] string searchString)
		{
			var result = await _customerService.GetCustomersAsync(searchString);
			return Ok(result);
		}

		/// <summary>
		/// Add customer
		/// </summary>
		/// <param name="customer"></param>
		// POST api/customers
		[HttpPost]
		public async Task<ActionResult> Post([FromBody] Customer customer)
		{
			await _customerService.AddCustomerAsync(customer);
			return Ok();



		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromBody] Customer customer)
		{
			try
			{
				await _customerService.UpdateCustomerAsync(customer, id);
				return Ok();
			}
			catch (HttpException ex)
			{
				if (ex.ResponseStatusCode == System.Net.HttpStatusCode.NotFound)
					return NotFound();
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
