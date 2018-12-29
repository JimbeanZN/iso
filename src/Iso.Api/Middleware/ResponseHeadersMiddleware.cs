using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Iso.Api.Middleware
{
  public class ResponseHeadersMiddleware
  {
    private readonly RequestDelegate _next;

    public ResponseHeadersMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      context.Response.OnStarting(state =>
      {
        context.Response.Headers.Add("X-Api-Version", Assembly.GetEntryAssembly().GetName().Version.ToString());
        return Task.FromResult(0);
      }, context);

      await _next(context);
    }
  }
}