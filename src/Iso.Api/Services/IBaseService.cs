using System.Collections.Generic;
using System.Threading.Tasks;
using Iso.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services
{
  public interface IBaseService<out T> where T : BaseModel
  {
    IEnumerable<T> Data { get; }
    Task<IActionResult> GetAsync();
    Task<IActionResult> GetFromAlpha3CodeAsync(string alpha3Code);
    Task<IActionResult> GetFromNumericCodeAsync(string numericCode);
    Task<IActionResult> GetRelatedEntities(string alpha3Code);
  }
}