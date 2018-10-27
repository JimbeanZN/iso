using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iso.Api.Extensions;
using Iso.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services
{
  internal class CountriesService : BaseService<IsoCountry>
  {
    private readonly IEnumerable<IsoCurrency> _currencies;

    public CountriesService(IEnumerable<IsoCountry> countries, IEnumerable<IsoCurrency> currencies) : base(countries)
    {
      _currencies = currencies;
    }

    public override async Task<IActionResult> GetRelatedEntities(string alpha3Code)
    {
      if (string.IsNullOrWhiteSpace(alpha3Code))
      {
        throw new ArgumentNullException(nameof(alpha3Code));
      }

      if (!alpha3Code.IsValidAlpha3Code())
      {
        throw new ArgumentException(nameof(alpha3Code));
      }

      var countryResult = Data.FirstOrDefault(country =>
        string.Equals(country.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

      var result = _currencies.Where(currency => currency.Countries.Contains(countryResult.Name));

      return await Task.FromResult((IActionResult) new OkObjectResult(result));
    }
  }
}