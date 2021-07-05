using System.IO;
using ApiDataLayer.DbContexts;
using ApiDataLayer.Models;
using ApiDataLayer.Models.RequestModels;
using ApiLogicLayer;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiLogicLayer.Swagger;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;

namespace UserApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers(t => t.Filters.Add(typeof(CustomModelValidator)));

			services.AddDbContext<UserApiDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("DBConnectionString")));

			services.AddScoped<IPasswordValidator<User>, CustomPasswordValidator>();

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<UserApiDbContext>()
				.AddDefaultTokenProviders();
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.RequireHttpsMetadata = false;

					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidAudience = Configuration["JwtAuthentication:Audience"],
						ValidIssuer = Configuration["JwtAuthentication:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuthentication:Secret"]))
					};
				});
			services.AddFluentValidation(validator => validator.RegisterValidatorsFromAssemblyContaining<RegisterModelValidator>());
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "User API", Version = "v1" });
				c.OperationFilter<ParametersDescriber>();
				var file = Path.Combine(System.AppContext.BaseDirectory, "UserApi.xml");
				c.IncludeXmlComments(file);
			});

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSwagger();


			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
