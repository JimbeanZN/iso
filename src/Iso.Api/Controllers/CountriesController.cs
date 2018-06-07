using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Iso.Api.Models;
using Iso.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Controllers
{
	[Route("api/countries")]
	[Produces("application/json")]
	public class CountriesController : Controller
	{
		private readonly ICountriesService _countryService;

		public CountriesController(ICountriesService countryService)
		{
			_countryService = countryService;
		}

		/// <summary>
		/// Gets a list of ISO Countries.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<IsoCountry>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get()
		{
			return await _countryService.GetAsync();
		}

		/// <summary>
		/// Gets an ISO Country by the ISO Alpha-3 Code.
		/// </summary>
		/// <param name="alpha3Code">The Alpha-3 code.</param>
		/// <returns></returns>
		[HttpGet("{alpha3Code:regex((?i)[[A-Z]]{{3}})}")]
		[ProducesResponseType(typeof(IsoCountry), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetFromAlpha3Code(string alpha3Code)
		{
			return await _countryService.GetFromAlpha3CodeAsync(alpha3Code);

		}

		/// <summary>
		/// Gets an ISO Country by the ISO Numeric Code.
		/// </summary>
		/// <param name="numericCode">The Numeric code.</param>
		/// <returns></returns>
		[HttpGet("{numericCode:regex([[0-9]]{{3}})}")]
		[ProducesResponseType(typeof(IsoCountry), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> GetFromNumericCode(string numericCode)
		{
			return await _countryService.GetFromNumericCodeAsync(numericCode);
		}

		/// <summary>
		/// Gets a list ISO Currencies for the given ISO Country.
		/// </summary>
		/// <param name="alpha3Code">The Alpha-3 code.</param>
		/// <returns></returns>
		[HttpGet("{alpha3Code:regex((?i)[[A-Z]]{{3}})}/currencies")]
		[ProducesResponseType(typeof(IEnumerable<IsoCurrency>), (int) HttpStatusCode.OK)]
		public async Task<IActionResult> GetCurrencies(string alpha3Code)
		{
			return await _countryService.GetCurrenciesAsync(alpha3Code);
		}
	}
}