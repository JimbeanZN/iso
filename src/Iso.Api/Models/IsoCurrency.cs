using System.Collections.Generic;
using Iso.Api.DataAnnotations;

namespace Iso.Api.Models
{
  public class IsoCurrency : BaseModel
  {
    public IsoCurrency(string name, string isoAlpha3Code, string isoNumericCode, int isoExponent,
      IEnumerable<string> countries) : base(name, isoAlpha3Code, isoNumericCode)
    {
      IsoExponent = isoExponent;
      Countries = countries;
    }

    public int IsoExponent { get; }

    internal IEnumerable<string> Countries { get; }
  }

  internal class ItermediaryIsoCurrency : BaseModel
  {
    public ItermediaryIsoCurrency(string name, string isoAlpha3Code, string isoNumericCode, int isoExponent, string countryName) : 
      base(name, isoAlpha3Code, isoNumericCode)
    {
      IsoExponent = isoExponent;
      CountryName = countryName;
    }

    public int IsoExponent { get; set; }

    public string CountryName { get; set; }
  }
}