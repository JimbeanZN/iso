using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iso.StatefulApi.Models
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

		[MaxLength(2)]
		[MinLength(2)]
		public string IsoAlpha2Code { get; }

		[MaxLength(3)]
		[MinLength(3)]
		public string IsoAlpha3Code { get; }

		[MaxLength(3)]
		[MinLength(3)]
		public string IsoNumericCode { get; }
	}
}
