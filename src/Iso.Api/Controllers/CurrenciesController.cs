using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Iso.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Controllers
{
	/// <inheritdoc />
	[Route("api/currencies")]
	[Produces("application/json")]
	public class CurrenciesController : Controller
	{
		private readonly IEnumerable<IsoCurrency> _currencies;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="currencies"></param>
		public CurrenciesController(IEnumerable<IsoCurrency> currencies)
		{
			_currencies = currencies;
		}

		/// <summary>
		/// Gets a list of ISO Currencies.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<IsoCurrency>), (int)HttpStatusCode.OK)]
		public IActionResult Get()
		{
			return new OkObjectResult(_currencies.ToList());
		}

		/// <summary>
		/// Gets an ISO Currency by the ISO Alpha-3 Code.
		/// </summary>
		/// <param name="alpha3Code">The Alpha-3 code.</param>
		/// <returns></returns>
		[HttpGet("{alpha3Code:regex((?i)[[A-Z]]{{3}})}")]
		[ProducesResponseType(typeof(IsoCurrency), (int)HttpStatusCode.OK)]
		public IActionResult GetFromAlpha3Code(string alpha3Code)
		{
			var result = _currencies.FirstOrDefault(country =>
				string.Equals(country.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

			return result != null ? (IActionResult)new OkObjectResult(result) : new NotFoundResult();

		}

		/// <summary>
		/// Gets an ISO Currency by the ISO Numeric Code.
		/// </summary>
		/// <param name="numericCode">The Numeric code.</param>
		/// <returns></returns>
		[HttpGet("{numericCode:regex([[0-9]]{{3}})}")]
		[ProducesResponseType(typeof(IsoCurrency), (int)HttpStatusCode.OK)]
		public IActionResult GetFromNumericCode(string numericCode)
		{
			var result = _currencies.FirstOrDefault(country =>
				string.Equals(country.IsoNumericCode, numericCode, StringComparison.OrdinalIgnoreCase));

			return result != null ? (IActionResult)new OkObjectResult(result) : new NotFoundResult();
		}
	}
}