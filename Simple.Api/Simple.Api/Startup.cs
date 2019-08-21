using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Simple.Api.Repositories;
using Simple.Api.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Simple.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var application = new ApplicationSettings();
			Configuration.GetSection("Application").Bind(application);
			services.AddSingleton(application);

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddSwaggerGen(options =>
			{
				
				// UseFullTypeNameInSchemaIds replacement for .NET Core
				options.CustomSchemaIds(x => x.FullName);

				options.SwaggerDoc("v1", new Info
				{
					Title = application.Name,
					Version = application.Version
				});
				var xmlFilename = $"{AppDomain.CurrentDomain.FriendlyName}.xml";
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFilename);
				options.IncludeXmlComments(filePath);
								
			});

			services.AddDbContext<SimpleApiDbContext>(opt => opt.UseInMemoryDatabase("SimpleApi"));
			services.AddTransient<ICustomerRepository, CustomerRepository>();
			services.AddTransient<ICustomerService, CustomerService>();
		}

		/// <summary>
		/// config swagger and basic settings
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		/// <param name="applicationSettings"></param>
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationSettings applicationSettings)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}			
			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json",
					$"{applicationSettings.Name} {applicationSettings.Version}");
			});
				
		}
	}
}
