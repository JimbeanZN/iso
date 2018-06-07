using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Iso.Api.Models;

namespace Iso.Api.Entities
{
	public class Countries : IEnumerable<IsoCountry>
	{
		private static List<IsoCountry> _dataSet;

		public Countries()
		{
			var fileStream = new FileStream(@"data\countries.csv", FileMode.Open, FileAccess.Read);
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
			{
				var csv = new CsvReader(streamReader);
				csv.Configuration.HasHeaderRecord = false;
				_dataSet = csv.GetRecords<IsoCountry>().ToList();
			}
		}

		/// <summary>
		///   Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		///   An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<IsoCountry> GetEnumerator()
		{
			return _dataSet.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}