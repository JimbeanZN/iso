using System;
using System.Collections.Generic;
using System.IO;
using Iso.Api.Entities;
using Iso.Api.Filters;
using Iso.Api.Models;
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
			services.AddSingleton<IEnumerable<IsoCountry>, Countries>();
			services.AddSingleton<IEnumerable<IsoCurrency>, Currencies>();

			services.AddMvc().AddJsonOptions(options => { options.SerializerSettings.Formatting = Formatting.Indented; });

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(Version, new Info
				{
					Version = Version,
					Title = Title,
					Description = "A simple example ASP.NET Core Web API to eexpose ISO data",
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

			app.UseMvc();
		}
	}
}