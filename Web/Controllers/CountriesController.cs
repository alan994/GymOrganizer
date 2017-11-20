using Data.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("api/countries")]
    public class CountriesController : AuthController
    {
        public readonly ILogger<CountriesController> Logger;
        public readonly CountryService CountryService;

        public CountriesController(ILogger<CountriesController> logger, CountryService countryService, GymOrganizerContext db) : base(db)
        {
            this.Logger = logger;
            this.CountryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CountryVM> resultList = await this.CountryService.GetAllCountries();
            return Ok(resultList);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            List<CountryVM> resultList = await this.CountryService.GetAllActiveCountries();
            return Ok(resultList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            CountryVM country = await this.CountryService.GetCountryById(id);
            return Json(country);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CountryVM country)
        {
            await this.CountryService.AddCountry(country);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]CountryVM country)
        {
            await this.CountryService.EditCountry(country);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.CountryService.DeleteCountry(id);
            return Ok();
        }
    }
}
