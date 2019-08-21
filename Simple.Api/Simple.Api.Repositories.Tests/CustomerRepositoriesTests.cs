using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace Simple.Api.Repositories.Tests
{
	/// <summary>
	/// This is testing using inMemory database of EFCore instead of mock
	/// </summary>
	public class CustomerRepositoriesTests
	{
		private SimpleApiDbContext _dbContext;
		private ICustomerRepository _customerRepository;
		public CustomerRepositoriesTests(SimpleApiDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[Fact]
		public async Task AddCustomerSuccessfully()
		{
			var today = DateTime.Now;
			var totalRecords = _dbContext.Customers.Count();
			_customerRepository = new CustomerRepository(_dbContext);
			var result = await _customerRepository.AddCustomerAsync(new Model.Customer() { FirstName =  "Helen", LastName = "Anna", DateOfBirth = today });
			var currentRecords = _dbContext.Customers.Count();
			Assert.True(result > 0, "Result should be greater than 0");
			Assert.Equal(totalRecords + 1, currentRecords);

		}


		[Fact]
		public async Task UpdateCustomerSuccessfully()
		{
			// Arrange
			var today = DateTime.Now;
			var customer = new Model.Customer() { FirstName = "Oshin", LastName = "Kelemi", DateOfBirth = today };
			_dbContext.Customers.Add(customer);
			_dbContext.SaveChanges();
			var id = customer.CustomerId;
			var totalRecords = _dbContext.Customers.Count();

			_customerRepository = new CustomerRepository(_dbContext);

			var updatedCustomer = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == id);
			updatedCustomer.FirstName = "Steve";
			updatedCustomer.LastName = "Alex";
			updatedCustomer.DateOfBirth = today.AddDays(2);

			// Action
			await _customerRepository.UpdateCustomerAsync(updatedCustomer);

			// Assert
			var currentRecords = _dbContext.Customers.Count();
			var expectedCustomer  = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == id);
			Assert.Equal(totalRecords, currentRecords);
			Assert.Equal(updatedCustomer, expectedCustomer);

		}

		[Fact]
		public async Task DeleteCustomerSuccessfully()
		{
			// Arrange
			var today = DateTime.Now;
			var customer = new Model.Customer() { FirstName = "Anthony", LastName = "Blade", DateOfBirth = today };
			_dbContext.Customers.Add(customer);
			_dbContext.SaveChanges();
			var id = customer.CustomerId;
			var totalRecords = _dbContext.Customers.Count();

			_customerRepository = new CustomerRepository(_dbContext);

			var deletedCustomer = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == id);

			// Action
			await _customerRepository.DeleteCustomerAsync(deletedCustomer);

			// Assert
			var currentRecords = _dbContext.Customers.Count();
			var expectedCustomer = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == id);
			Assert.Equal(totalRecords-1, currentRecords);
			Assert.Null(expectedCustomer);

		}

		[Fact]
		public async Task GetCustomerByIdSuccessfully()
		{
			// Arrange
			var today = DateTime.Now;
			var customer = new Model.Customer() { FirstName = "Emanual", LastName = "Sam", DateOfBirth = today };
			_dbContext.Customers.Add(customer);
			_dbContext.SaveChanges();
			var id = customer.CustomerId;
			_customerRepository = new CustomerRepository(_dbContext);
			
			// Action
			var expected = await _customerRepository.GetCustomerAsync(id);

			// Assert
			
			Assert.Equal(customer.FirstName, expected.FirstName);
			Assert.Equal(customer.LastName, expected.LastName);
			Assert.Equal(today, expected.DateOfBirth);
			
		}

		[Fact]
		public async Task GetCustomerByIdNotFoundReturnsNull()
		{
			// Arrange
		
			_customerRepository = new CustomerRepository(_dbContext);

			// Action
			var expected = await _customerRepository.GetCustomerAsync(100000);

			// Assert

			Assert.Null(expected);

		}
	}
}
