using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services.Countries
{
	public interface ICountriesService
	{
		Task<IActionResult> GetAsync();
		Task<IActionResult> GetFromAlpha3CodeAsync(string alpha3Code);
		Task<IActionResult> GetFromNumericCodeAsync(string numericCode);
		Task<IActionResult> GetCurrenciesAsync(string alpha3Code);
	}
}