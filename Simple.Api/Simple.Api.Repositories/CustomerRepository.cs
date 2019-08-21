using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Simple.Api.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private SimpleApiDbContext _dbContext;
		public CustomerRepository(SimpleApiDbContext dbContext) {
			_dbContext = dbContext;
		}
		public async Task<int> AddCustomerAsync(Customer entity)
		{
			_dbContext.Add(entity);
			return await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Customer>>  GetCustomersAsync()
		{
			return await Task.FromResult<IEnumerable<Customer>>(_dbContext.Customers);
		}

		public async Task UpdateCustomerAsync(Customer entity)
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task<Customer> GetCustomerAsync(int id)
		{
			return await _dbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
		}
	}
}
