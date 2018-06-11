using Iso.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Iso.Api.Extensions
{
	public static class ResponseHeadersMiddlewareExtensions
	{
		public static IApplicationBuilder UseResponseHeaders(
			this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ResponseHeadersMiddleware>();
		}
	}
}
