using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Iso.Api.Data;
using Iso.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.Formatting = Formatting.Indented;
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(Version, new Info
				{
					Version = Version,
					Title = Title,
					Description = "A simple example ASP.NET Core Web API to eexpose ISO data",
					TermsOfService = "None",
					Contact = new Contact { Name = "Shayne Boyer", Email = "", Url = "https://twitter.com/spboyer" },
					License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
				});

				var basePath = AppContext.BaseDirectory;
				var xmlPath = Path.Combine(basePath, "Iso.Api.xml");
				c.IncludeXmlComments(xmlPath);
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
