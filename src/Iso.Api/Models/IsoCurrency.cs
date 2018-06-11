using System.Collections.Generic;
using Iso.Api.DataAnnotations;

namespace Iso.Api.Models
{
	public class IsoCurrency
	{
		public IsoCurrency(string currencyName, string isoAlpha3Code, string isoNumericCode, int isoExponent,
			IEnumerable<string> countries)
		{
			CurrencyName = currencyName;
			IsoAlpha3Code = isoAlpha3Code;
			IsoNumericCode = isoNumericCode;
			IsoExponent = isoExponent;
			Countries = countries;
		}

		public string CurrencyName { get; }

		[AbsoluteLength(3)]
		public string IsoAlpha3Code { get; }

		[AbsoluteLength(3)]
		public string IsoNumericCode { get; }

		public int IsoExponent { get; }

		internal IEnumerable<string> Countries { get; }
	}

	internal class ItermediaryIsoCurrency
	{
		public string CurrencyName { get; set; }

		[AbsoluteLength(3)]
		public string IsoAlpha3Code { get; set; }

		[AbsoluteLength(3)]
		public string IsoNumericCode { get; set; }

		public int IsoExponent { get; set; }

		public string CountryName { get; set; }
	}
}