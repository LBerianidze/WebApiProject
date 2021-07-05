using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiLogicLayer.Swagger
{
	public class ParametersDescriber : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.Parameters == null)
			{
				operation.Parameters = new List<OpenApiParameter>();
			}

			var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

			if (descriptor == null)
				return;
			if (descriptor.ActionName == "Register")
			{

				operation.Parameters.Add(new OpenApiParameter() { Name = "Email", In = ParameterLocation.Query, Description = "User email", Required = true });
				operation.Parameters.Add(new OpenApiParameter() { Name = "UserName", In = ParameterLocation.Query, Description = "User login", Required = true });
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "Password",
					In = ParameterLocation.Query,
					Description = "User password",
					Required = true,
					Schema = new OpenApiSchema() { MinLength = 6 }
				});

				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "PersonalNumber",
					In = ParameterLocation.Query,
					Description = "User personal number",
					Required = true,
					Schema = new OpenApiSchema() { MinLength = 11, MaxLength = 11 },
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "IsMarried",
					In = ParameterLocation.Query,
					Description = "Is user married or not",
					Required = true,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "IsWorking",
					In = ParameterLocation.Query,
					Description = "Is user working or not",
					Required = true,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "Salary",
					In = ParameterLocation.Query,
					Description = "User salary if working",
					Required = false,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "Country",
					In = ParameterLocation.Query,
					Description = "User country",
					Required = true,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "City",
					In = ParameterLocation.Query,
					Description = "User city",
					Required = true,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "Street",
					In = ParameterLocation.Query,
					Description = "User street",
					Required = true,
				});
			}

			if (descriptor.ActionName == "Login")
			{

				operation.Parameters.Add(new OpenApiParameter() { Name = "Email", In = ParameterLocation.Query, Description = "User email", Required = true });

				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "Password",
					In = ParameterLocation.Query,
					Description = "User password",
					Required = true,
					Schema = new OpenApiSchema() { MinLength = 6 }
				});
			}

			if (descriptor.ActionName == "Update")
			{

				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "IsMarried",
					In = ParameterLocation.Query,
					Description = "Is user married or not",
					Required = true,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "IsWorking",
					In = ParameterLocation.Query,
					Description = "Is user working or not",
					Required = true,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "Salary",
					In = ParameterLocation.Query,
					Description = "User salary if working",
					Required = false,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "Country",
					In = ParameterLocation.Query,
					Description = "User country",
					Required = true,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "City",
					In = ParameterLocation.Query,
					Description = "User city",
					Required = true,
				});
				operation.Parameters.Add(new OpenApiParameter()
				{
					Name = "Street",
					In = ParameterLocation.Query,
					Description = "User street",
					Required = true,
				});

			}
		}
	}
}