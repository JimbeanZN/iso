using System.Text.RegularExpressions;

namespace Iso.Api.Extensions
{
	internal static class StringExtensions
	{
		public static bool IsValidAlpha3Code(this string value)
		{
			var regex = new Regex("(?i)[A-Z]{3}");
			return regex.IsMatch(value);
		}

		public static bool IsValidNumericCode(this string value)
		{
			var regex = new Regex("[0-9]{3}");
			return regex.IsMatch(value);
		}
	}
}
