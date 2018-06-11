using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Iso.Api.Models;
using Iso.Api.Services.Currencies;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Controllers
{
	[Route("api/currencies")]
	[Produces("application/json")]
	public class CurrenciesController : Controller
	{
		private readonly ICurrenciesService _currenciesService;

		internal CurrenciesController(ICurrenciesService currenciesService)
		{
			_currenciesService = currenciesService;
		}

		/// <summary>
		///   Gets a list of ISO Currencies.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<IsoCurrency>), (int) HttpStatusCode.OK)]
		public async Task<IActionResult> Get()
		{
			return await _currenciesService.GetAsync();
		}

		/// <summary>
		///   Gets an ISO Currency by the ISO Alpha-3 Code.
		/// </summary>
		/// <param name="alpha3Code">The Alpha-3 code.</param>
		/// <returns></returns>
		[HttpGet("{alpha3Code:regex((?i)[[A-Z]]{{3}})}")]
		[ProducesResponseType(typeof(IsoCurrency), (int) HttpStatusCode.OK)]
		public async Task<IActionResult> GetFromAlpha3Code(string alpha3Code)
		{
			return await _currenciesService.GetFromAlpha3CodeAsync(alpha3Code);
		}

		/// <summary>
		///   Gets an ISO Currency by the ISO Numeric Code.
		/// </summary>
		/// <param name="numericCode">The Numeric code.</param>
		/// <returns></returns>
		[HttpGet("{numericCode:regex([[0-9]]{{3}})}")]
		[ProducesResponseType(typeof(IsoCurrency), (int) HttpStatusCode.OK)]
		public async Task<IActionResult> GetFromNumericCode(string numericCode)
		{
			return await _currenciesService.GetFromNumericCodeAsync(numericCode);
		}

		/// <summary>
		///   Gets a list ISO Countries for the given ISO Currency.
		/// </summary>
		/// <param name="alpha3Code">The Alpha-3 code.</param>
		/// <returns></returns>
		[HttpGet("{alpha3Code:regex((?i)[[A-Z]]{{3}})}/currencies")]
		[ProducesResponseType(typeof(IEnumerable<IsoCountry>), (int) HttpStatusCode.OK)]
		public async Task<IActionResult> GetCurrencies(string alpha3Code)
		{
			return await _currenciesService.GetCountriesAsync(alpha3Code);
		}
	}
}