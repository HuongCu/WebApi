using Simple.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Api.Repositories
{
	public interface ICustomerRepository
	{
	    Task<int> AddCustomerAsync(Customer entity);

		Task UpdateCustomerAsync(Customer entity);

		Task<IEnumerable<Customer>> GetCustomersAsync();

		Task<Customer> GetCustomerAsync(int id);
	}
}
