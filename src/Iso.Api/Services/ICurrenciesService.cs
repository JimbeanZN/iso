using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services
{
	public interface ICurrenciesService
	{
		Task<IActionResult> GetAsync();
		Task<IActionResult> GetFromAlpha3CodeAsync(string alpha3Code);
		Task<IActionResult> GetFromNumericCodeAsync(string numericCode);
		Task<IActionResult> GetCountriesAsync(string alpha3Code);
	}
}