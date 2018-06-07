using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iso.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services
{
	internal class CountriesService : ICountriesService
	{
		private readonly IEnumerable<IsoCountry> _countries;
		private readonly IEnumerable<IsoCurrency> _currencies;

		public CountriesService(IEnumerable<IsoCountry> countries, IEnumerable<IsoCurrency> currencies)
		{
			_countries = countries;
			_currencies = currencies;
		}

		public async Task<IActionResult> GetAsync()
		{
			return await Task.FromResult(new OkObjectResult(_countries.ToList()));
		}

		public async Task<IActionResult> GetFromAlpha3CodeAsync(string alpha3Code)
		{
			if (string.IsNullOrWhiteSpace(alpha3Code))
			{
				throw new ArgumentNullException(nameof(alpha3Code));
			}

			var result = _countries.FirstOrDefault(country =>
				string.Equals(country.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

			return await Task.FromResult(result != null ? (IActionResult) new OkObjectResult(result) : new NotFoundResult());
		}

		public async Task<IActionResult> GetFromNumericCodeAsync(string numericCode)
		{
			if (string.IsNullOrWhiteSpace(numericCode))
			{
				throw new ArgumentNullException(nameof(numericCode));
			}

			var result = _countries.FirstOrDefault(country =>
				string.Equals(country.IsoNumericCode, numericCode, StringComparison.OrdinalIgnoreCase));

			return await Task.FromResult(result != null ? (IActionResult) new OkObjectResult(result) : new NotFoundResult());
		}

		public async Task<IActionResult> GetCurrenciesAsync(string alpha3Code)
		{
			if (string.IsNullOrWhiteSpace(alpha3Code))
			{
				throw new ArgumentNullException(nameof(alpha3Code));
			}

			var countryResult = _countries.FirstOrDefault(country =>
				string.Equals(country.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

			var result = _currencies.Where(currency => currency.Countries.Contains(countryResult.CountryName));

			return await Task.FromResult((IActionResult)new OkObjectResult(result));
		}
	}
}