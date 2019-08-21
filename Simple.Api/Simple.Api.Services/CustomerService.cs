using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Simple.Api.Model;
using Simple.Api.Repositories;
using System.Linq;
using System.Web;
using Simple.Api.Common;

namespace Simple.Api.Services
{
	public class CustomerService : ICustomerService
	{
		private ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}
		public async Task<int> AddCustomerAsync(Customer customer)
		{
			return await _customerRepository.AddCustomerAsync(customer);
		}

		public async Task DeleteCustomerAsync(int id)
		{
			var persistedCustomer = await _customerRepository.GetCustomerAsync(id);
			if (persistedCustomer == null)
			{
				throw new HttpException("Customer Not Found", System.Net.HttpStatusCode.NotFound);
			}

			await _customerRepository.DeleteCustomerAsync(persistedCustomer);

		}

		/// <summary>
		/// Search customer by partial firstname Or LastName
		/// </summary>
		/// <param name="searchString"></param>
		/// <returns></returns>
		public async Task<IList<Customer>> GetCustomersAsync(string searchString)
		{
			var customers = await _customerRepository.GetCustomersAsync();
			return customers.Where(x => (x.FirstName != null && x.FirstName.Equals(searchString))
										|| (x.LastName!=null &&  x.LastName.Equals(searchString)))?.ToList();
		}

		public async Task UpdateCustomerAsync(Customer customer, int id)
		{
			var persistedCustomer = await _customerRepository.GetCustomerAsync(id);
			if (persistedCustomer == null)
			{
				throw new HttpException("Customer Not Found", System.Net.HttpStatusCode.NotFound);
			}

			persistedCustomer.FirstName = customer.FirstName;
			persistedCustomer.LastName = customer.LastName;
			persistedCustomer.DateOfBirth = customer.DateOfBirth;

			await _customerRepository.UpdateCustomerAsync(customer);
		}
	}
}
