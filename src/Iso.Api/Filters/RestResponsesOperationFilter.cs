using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Iso.Api.Filters
{
	/// <inheritdoc />
	public class RestResponsesOperationFilter : IOperationFilter
	{
		public void Apply(Operation operation, OperationFilterContext context)
		{
			var httpGetAttributes = context.ApiDescription
				.ControllerAttributes()
				.Union(context.ApiDescription.ActionAttributes())
				.OfType<HttpGetAttribute>();

			if (httpGetAttributes.Any())
				operation.Responses.Add(((int)HttpStatusCode.NotFound).ToString(), new Response { Description = "Not Found" });
		}
	}
}
