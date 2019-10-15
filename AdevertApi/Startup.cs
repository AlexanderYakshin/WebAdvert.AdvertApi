using AdevertApi.HealthChecks;
using AdevertApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace AdevertApi
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
			//Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
			//ServiceCollectionExtensions.AddAutoMapper(services);
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddTransient<IAdvertStorageService, DynamoDbAdvertStorage>();

			services.AddControllers();
			services.AddHealthChecks()
				.AddCheck<StorageHealthCheck>("Storage", HealthStatus.Unhealthy, new List<string>(), new TimeSpan(0,1,0));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			//app.UseHealthChecks("/health");
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/health");
				endpoints.MapControllers();
			});
		}
	}
}
