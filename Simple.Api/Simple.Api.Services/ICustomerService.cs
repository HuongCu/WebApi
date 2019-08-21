using Simple.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simple.Api.Services
{
	public interface ICustomerService
	{
		Task<int> AddCustomerAsync(Customer customer);

		Task UpdateCustomerAsync(Customer customer, int customerId);

		Task<IList<Customer>> GetCustomersAsync(string searchString);

		Task DeleteCustomerAsync(int id);
	}
}
