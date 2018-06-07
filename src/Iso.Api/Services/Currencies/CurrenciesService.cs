using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iso.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services.Currencies
{
	internal class CurrenciesService : ICurrenciesService
	{
		private readonly IEnumerable<IsoCurrency> _currencies;
		private readonly IEnumerable<IsoCountry> _countries;

		public CurrenciesService(IEnumerable<IsoCurrency> currencies, IEnumerable<IsoCountry> countries)
		{
			_currencies = currencies;
			_countries = countries;
		}

		public async Task<IActionResult> GetAsync()
		{
			return await Task.FromResult(new OkObjectResult(_currencies.ToList()));
		}

		public async Task<IActionResult> GetFromAlpha3CodeAsync(string alpha3Code)
		{
			if (string.IsNullOrWhiteSpace(alpha3Code))
			{
				throw new ArgumentNullException(nameof(alpha3Code));
			}

			var result = _currencies.FirstOrDefault(country =>
				string.Equals(country.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

			return await Task.FromResult(result != null ? (IActionResult) new OkObjectResult(result) : new NotFoundResult());
		}

		public async Task<IActionResult> GetFromNumericCodeAsync(string numericCode)
		{
			if (string.IsNullOrWhiteSpace(numericCode))
			{
				throw new ArgumentNullException(nameof(numericCode));
			}

			var result = _currencies.FirstOrDefault(country =>
				string.Equals(country.IsoNumericCode, numericCode, StringComparison.OrdinalIgnoreCase));

			return await Task.FromResult(result != null ? (IActionResult) new OkObjectResult(result) : new NotFoundResult());
		}

		public async Task<IActionResult> GetCountriesAsync(string alpha3Code)
		{
			if (string.IsNullOrWhiteSpace(alpha3Code))
			{
				throw new ArgumentNullException(nameof(alpha3Code));
			}

			var currencyResult = _currencies.FirstOrDefault(currency =>
				string.Equals(currency.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

			var result = _countries.Where(country => currencyResult.Countries.Contains(country.CountryName));

			return await Task.FromResult((IActionResult)new OkObjectResult(result));
		}
	}
}