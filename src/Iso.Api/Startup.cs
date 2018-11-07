using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Iso.Api.Entities;
using Iso.Api.Extensions;
using Iso.Api.Models;
using Iso.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Iso.Api
{
  public class Startup
  {
    private const string Title = "ISO Api";
    private const string Version = "v1";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      ConfigureSwagger(services);
      ConfigureIoC(services);

      services.AddApplicationInsightsTelemetry(Configuration);

      services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; });
    }

    private static void ConfigureSwagger(IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc(Version, new Info
        {
          Version = Version,
          Title = Title,
          Description = "A simple ASP.NET Core Web API to expose ISO data",
          TermsOfService = "None"
        });

        var basePath = AppContext.BaseDirectory;
        var xmlPath = Path.Combine(basePath, "Iso.Api.xml");
        c.IncludeXmlComments(xmlPath);

        c.DescribeAllEnumsAsStrings();
        c.DescribeStringEnumsInCamelCase();

        c.OperationFilter<HulkOut.AspNetCore.Swashbuckle.Filters.InternalServerErrorResponseOperationFilter>();
        c.OperationFilter<HulkOut.AspNetCore.Swashbuckle.Filters.GetResponseOperationFilter>();
      });
    }

    private static void ConfigureIoC(IServiceCollection services)
    {
      services.AddSingleton<IEnumerable<IsoCountry>, Countries>();
      services.AddSingleton<IEnumerable<IsoCurrency>, Currencies>();

      services.AddTransient<IBaseService<IsoCountry>, CountriesService>();
      services.AddTransient<IBaseService<IsoCurrency>, CurrenciesService>();
    }

    public static void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddApplicationInsights(app.ApplicationServices);

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.RoutePrefix = "api-docs";
        c.SwaggerEndpoint($"/swagger/{Version}/swagger.json", $"{Title} {Version.ToUpper(CultureInfo.InvariantCulture)}");
      });

      app.UseResponseHeaders();

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}