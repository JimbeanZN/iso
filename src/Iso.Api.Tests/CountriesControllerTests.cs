using System.Linq;
using System.Threading.Tasks;
using Iso.Api.Controllers;
using Iso.Api.Models;
using Iso.Api.Services;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Iso.Api.Tests
{
  public class CountriesControllerTests
  {
    private readonly IBaseService<IsoCountry> _baseService = Substitute.For<IBaseService<IsoCountry>>();

    private CountriesController CountriesController()
    {
      return new CountriesController(_baseService);
    }

    [Test]
    public async Task Get_GivenNoParams_ExpectedCallsServiceGetWithNoParams()
    {
      //arrange
      var countriesController = CountriesController();

      //act
      var task = await countriesController.Get();

      //assert
      await _baseService.Received().GetAsync();
    }
  }
}