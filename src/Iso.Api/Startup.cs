using System;
using System.Collections.Generic;
using System.IO;
using Iso.Api.Entities;
using Iso.Api.Extensions;
using Iso.Api.Filters;
using Iso.Api.Models;
using Iso.Api.Services.Countries;
using Iso.Api.Services.Currencies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

				c.OperationFilter<RestResponsesOperationFilter>();
			});
		}

		private static void ConfigureIoC(IServiceCollection services)
		{
			services.AddSingleton<IEnumerable<IsoCountry>, Countries>();
			services.AddSingleton<IEnumerable<IsoCurrency>, Currencies>();

			services.AddTransient<ICountriesService, CountriesService>();
			services.AddTransient<ICurrenciesService, CurrenciesService>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.RoutePrefix = "api-docs";
				c.SwaggerEndpoint($"/swagger/{Version}/swagger.json", $"{Title} {Version.ToUpper()}");
			});

			app.UseResponseHeaders();
			app.UseMvc();
		}
	}
}