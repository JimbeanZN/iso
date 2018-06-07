using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services.Countries
{
	public interface ICountriesService : IBaseService
	{
		Task<IActionResult> GetCurrenciesAsync(string alpha3Code);
	}
}