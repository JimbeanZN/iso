using Iso.Api.DataAnnotations;

namespace Iso.Api.Models
{
	public class IsoCountry
	{
		public IsoCountry(string countryName, string isoAlpha2Code, string isoAlpha3Code, string isoNumericCode)
		{
			CountryName = countryName;
			IsoAlpha2Code = isoAlpha2Code;
			IsoAlpha3Code = isoAlpha3Code;
			IsoNumericCode = isoNumericCode;
		}

		public string CountryName { get; }

		[AbsoluteLength(2)]
		public string IsoAlpha2Code { get; }

		[AbsoluteLength(3)]
		public string IsoAlpha3Code { get; }

		[AbsoluteLength(3)]
		public string IsoNumericCode { get; }
	}
}