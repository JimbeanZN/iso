using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Iso.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Controllers
{
	/// <inheritdoc />
	[Route("api/countries")]
	[Produces("application/json")]
	public class CountriesController : Controller
	{
		private readonly IEnumerable<IsoCountry> _countries;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="countries"></param>
		public CountriesController(IEnumerable<IsoCountry> countries)
		{
			_countries = countries;
		}

		/// <summary>
		/// Gets a list of ISO Countries.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<IsoCountry>), (int)HttpStatusCode.OK)]
		public IActionResult Get()
		{
			return new OkObjectResult(_countries.ToList());
		}

		/// <summary>
		/// Gets an ISO Country by the ISO Alpha-2 Code.
		/// </summary>
		/// <param name="alpha2Code">The Alpha-2 code.</param>
		/// <returns></returns>
		[HttpGet("{alpha2Code:regex((?i)[[A-Z]]{{2}})}")]
		[ProducesResponseType(typeof(IsoCountry), (int)HttpStatusCode.OK)]
		public IActionResult GetFromAlpha2Code(string alpha2Code)
		{
			var result = _countries.FirstOrDefault(country =>
				string.Equals(country.IsoAlpha2Code, alpha2Code, StringComparison.OrdinalIgnoreCase));

			return result != null ? (IActionResult)new OkObjectResult(result) : new NotFoundResult();

		}

		/// <summary>
		/// Gets an ISO Country by the ISO Numeric Code.
		/// </summary>
		/// <param name="numericCode">The Numeric code.</param>
		/// <returns></returns>
		[HttpGet("{numericCode:regex([[0-9]]{{3}})}")]
		[ProducesResponseType(typeof(IsoCountry), (int)HttpStatusCode.OK)]
		public IActionResult GetFromNumericCode(string numericCode)
		{
			var result = _countries.FirstOrDefault(country =>
				string.Equals(country.IsoNumericCode, numericCode, StringComparison.OrdinalIgnoreCase));

			return result != null ? (IActionResult)new OkObjectResult(result) : new NotFoundResult();
		}
	}
}