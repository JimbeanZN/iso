using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services
{
	public interface IBaseService
	{
		Task<IActionResult> GetAsync();
		Task<IActionResult> GetFromAlpha3CodeAsync(string alpha3Code);
		Task<IActionResult> GetFromNumericCodeAsync(string numericCode);
	}
}