using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Iso.Api.Models;

namespace Iso.Api.Entities
{
	public class Currencies : IEnumerable<IsoCurrency>
	{
		private static List<IsoCurrency> _dataSet;

		public Currencies()
		{
			var fileStream = new FileStream(@"data\currencies.csv", FileMode.Open, FileAccess.Read);
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
			{
				var csv = new CsvReader(streamReader);
				csv.Configuration.HasHeaderRecord = false;
				var list = csv.GetRecords<ItermediaryIsoCurrency>().ToList();

				_dataSet = list.GroupBy(currency => currency.IsoAlpha3Code,
						(key, group) => new IsoCurrency(list.First(currency => currency.IsoAlpha3Code == key).CurrencyName,
							list.First(currency => currency.IsoAlpha3Code == key).IsoAlpha3Code,
							list.First(currency => currency.IsoAlpha3Code == key).IsoNumericCode,
							list.First(currency => currency.IsoAlpha3Code == key).IsoExponent,
							group.Select(currency => currency.CountryName).OrderBy(country => country)))
					.OrderBy(currency => currency.IsoAlpha3Code).ToList();
			}
		}

		/// <summary>
		///   Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		///   An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<IsoCurrency> GetEnumerator()
		{
			return _dataSet.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}