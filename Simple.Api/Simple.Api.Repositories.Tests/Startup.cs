using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;


[assembly: TestFramework("Simple.Api.Repositories.Tests.Startup", "Simple.Api.Repositories.Tests")]

namespace Simple.Api.Repositories.Tests
{
	public class Startup : DependencyInjectionTestFramework
	{
		public Startup(IMessageSink messageSink) : base(messageSink)
		{
		}

		protected override void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<SimpleApiDbContext>(opt => opt.UseInMemoryDatabase("SimpleApi"));
		}
	}
}
