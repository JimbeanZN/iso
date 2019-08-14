using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iso.Api.Extensions;
using Iso.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.Api.Services
{
  public abstract class BaseService<T> : IBaseService<T>
    where T : BaseModel
  {
    protected BaseService(IEnumerable<T> data)
    {
      Data = data;
    }

    public IEnumerable<T> Data { get; }

    public virtual async Task<IActionResult> GetAsync()
    {
      return await Task.FromResult(new OkObjectResult(Data.ToList()));
    }

    public virtual async Task<IActionResult> GetFromAlpha3CodeAsync(string alpha3Code)
    {
      if (string.IsNullOrWhiteSpace(alpha3Code))
      {
        throw new ArgumentNullException(nameof(alpha3Code));
      }

      if (!alpha3Code.IsValidAlpha3Code())
      {
        throw new ArgumentException(nameof(alpha3Code));
      }

      var result = Data.FirstOrDefault(item =>
        string.Equals(item.IsoAlpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

      return await Task.FromResult(result != null ? (IActionResult) new OkObjectResult(result) : new NotFoundResult());
    }

    public virtual async Task<IActionResult> GetFromNumericCodeAsync(string numericCode)
    {
      if (string.IsNullOrWhiteSpace(numericCode))
      {
        throw new ArgumentNullException(nameof(numericCode));
      }

      if (!numericCode.IsValidNumericCode())
      {
        throw new ArgumentException(nameof(numericCode));
      }

      var result = Data.FirstOrDefault(item =>
        string.Equals(item.IsoNumericCode, numericCode, StringComparison.OrdinalIgnoreCase));

      return await Task.FromResult(result != null ? (IActionResult) new OkObjectResult(result) : new NotFoundResult());
    }

    public abstract Task<IActionResult> GetRelatedEntities(string alpha3Code);
  }
}