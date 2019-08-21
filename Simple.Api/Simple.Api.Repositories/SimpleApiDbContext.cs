using Microsoft.EntityFrameworkCore;
using Simple.Api.Model;
using System;

namespace Simple.Api.Repositories
{
	public class SimpleApiDbContext : DbContext
	{
		public SimpleApiDbContext(DbContextOptions<SimpleApiDbContext> options)
			: base(options)
		{
		}

		public DbSet<Customer> Customers { get; set; }
		
	}
}
