using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using People.BussinesLogic.Blo.Interfaces;
using People.BussinesLogic.Services;
using People.DataAccess.Contexts;
using People.DataAccess.Repositories;
using People.DataAccess.Rto.Interfaces;

namespace People
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
			services.AddControllers().ConfigureApiBehaviorOptions(x => { x.SuppressMapClientErrors = true; });
			
			services.AddSwaggerDocument(config =>
			{
				config.PostProcess = document =>
				{
					document.Info.Version = "v1";
					document.Info.Title = "People API";
					document.Info.Description = "A simple ASP.NET Core web API";
				};
			});
			// services.AddSwaggerGen(c =>
			// {
			// 	c.SwaggerDoc("v1", new OpenApiInfo { Title = "People", Version = "v1" });
			// });
			
			string connectionString = Configuration.GetConnectionString(nameof(SqlPeopleContext));

			services.AddDbContext<SqlPeopleContext>(builder =>
					builder.UseSqlServer(connectionString, options =>
					{
						options.EnableRetryOnFailure(
							maxRetryCount: 12,
							maxRetryDelay: TimeSpan.FromSeconds(30),
							errorNumbersToAdd: null);
					}));
			
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddScoped<IPeopleRepository, PeopleRepository>();
			services.AddScoped<IPeopleService, PeopleService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseOpenApi();
				app.UseSwaggerUi3();
				// app.UseDeveloperExceptionPage();
				// app.UseSwagger();
				// app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "People v1"));
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}