using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iso.Api.Extensions;
using Iso.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services
{
  internal class CurrenciesService : BaseService<IsoCurrency>
  {
    private readonly IEnumerable<IsoCountry> _countries;
    
    public CurrenciesService(IEnumerable<IsoCurrency> currencies, IEnumerable<IsoCountry> countries) : base(currencies)
    {
      _countries = countries;
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

      var currencyResult = Data.FirstOrDefault(currency =>
        string.Equals(currency.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

      var result = _countries.Where(country => currencyResult.Countries.Contains(country.Name));

      return await Task.FromResult((IActionResult) new OkObjectResult(result)).ConfigureAwait(false);
    }
  }
}