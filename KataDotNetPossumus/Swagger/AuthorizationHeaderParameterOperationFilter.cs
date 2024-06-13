using KataDotNetPossumus.Resources;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KataDotNetPossumus.Api.Swagger;


public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		operation.Security ??= new List<OpenApiSecurityRequirement>();
		var scheme = new OpenApiSecurityScheme
		{
			Reference = new OpenApiReference
			{
				Type = ReferenceType.SecurityScheme,
				Id = Labels.Bearer
			}
		};
		operation.Security.Add(new OpenApiSecurityRequirement
		{
			[scheme] = new List<string>()
		});
	}
}