using System.Collections.Generic;
using Iso.StatefulApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iso.StatefulApi.Controllers
{
	[Route("api/countries")]
	public class CountriesController : Controller
	{
		[HttpGet]
		public IEnumerable<IsoCountry> Get()
		{
			return null;
		}

		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}
	}
}