using System;
using System.Collections.Generic;
using System.Linq;
using Iso.StatelessApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.StatelessApi.Controllers
{
	[Route("api/countries")]
	[Produces("application/json")]
	public class CountriesController : Controller
	{
		private readonly IEnumerable<IsoCountry> _countries;

		public CountriesController(IEnumerable<IsoCountry> countries)
		{
			_countries = countries;
		}

		/// <summary>
		/// Gets a list of ISO Countries.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Get()
		{
			return new OkObjectResult(_countries.ToList());
		}

		/// <summary>
		/// Gets an ISO Country by country name.
		/// </summary>
		/// <param name="countryName">Name of the country.</param>
		/// <returns></returns>
		[HttpGet("{countryName}")]
		public IActionResult Get(string countryName)
		{
			return new OkObjectResult(_countries.FirstOrDefault(country =>
				string.Equals(country.CountryName, countryName, StringComparison.OrdinalIgnoreCase)));
		}

		/// <summary>
		/// Gets an ISO Country by the ISO Alpha-2 Code.
		/// </summary>
		/// <param name="alpha2Code">The alpha2 code.</param>
		/// <returns></returns>
		[HttpGet("fromAlpha2Code/{alpha2Code}")]
		public IActionResult GetFromAlpha2Code(string alpha2Code)
		{
			var result = _countries.FirstOrDefault(country =>
				string.Equals(country.IsoAlpha2Code, alpha2Code, StringComparison.OrdinalIgnoreCase));

			return result != null ? (IActionResult) new OkObjectResult(result) : new NotFoundResult();

		}

		/// <summary>
		/// Gets an ISO Country by the ISO Alpha-3 Code.
		/// </summary>
		/// <param name="alpha3Code">The alpha3 code.</param>
		/// <returns></returns>
		[HttpGet("fromAlpha3Code/{alpha3Code}")]
		public IActionResult GetFromAlpha3Code(string alpha3Code)
		{
			var result = _countries.FirstOrDefault(country =>
				string.Equals(country.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

			return result != null ? (IActionResult)new OkObjectResult(result) : new NotFoundResult();
		}

		/// <summary>
		/// Gets an ISO Country by the ISO Numeric Code.
		/// </summary>
		/// <param name="numericCode">The numeric code.</param>
		/// <returns></returns>
		[HttpGet("fromNumericCode/{numericCode}")]
		public IActionResult GetFromNumericCode(string numericCode)
		{
			var result = _countries.FirstOrDefault(country =>
				string.Equals(country.IsoNumericCode, numericCode, StringComparison.OrdinalIgnoreCase));

			return result != null ? (IActionResult)new OkObjectResult(result) : new NotFoundResult();
		}
	}
}