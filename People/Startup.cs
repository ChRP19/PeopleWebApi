using System;
using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using People.BussinesLogic.Blo.Interfaces;
using People.BussinesLogic.Services;
using People.DataAccess.Contexts;
using People.DataAccess.Repositories;
using People.DataAccess.Rto.Interfaces;
using People.Middleware;

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

			services.AddValidatorsFromAssembly(Assembly.Load("People.BussinesLogic"));

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["Jwt:Issuer"],
						ValidAudience = Configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey
							(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
					};
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseOpenApi();
				app.UseSwaggerUi3();
			}

			app.UseHttpsRedirection();
			
			app.UseRouting();

			app.UseAuthentication();
			
			app.UseAuthorization();

			app.UseMiddleware<ApiKeyMiddleware>();
			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}