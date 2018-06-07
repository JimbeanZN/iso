using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services.Currencies
{
	public interface ICurrenciesService : IBaseService
	{
		Task<IActionResult> GetCountriesAsync(string alpha3Code);
	}
}