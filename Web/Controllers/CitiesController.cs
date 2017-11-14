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
    [Route("api/cities")]
    public class CitiesController : AuthController
    {
        public readonly ILogger<CitiesController> Logger;
        public readonly CityService CityService;

        public CitiesController(ILogger<CitiesController> logger, CityService cityService, GymOrganizerContext db) : base(db)
        {
            this.Logger = logger;
            this.CityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CityVM> resultList = await this.CityService.GetAllCitiesAsync();
            return Ok(resultList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            CityVM city = await this.CityService.GetCityByIdAsync(id);
            return Json(city);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CityVM city)
        {
            await this.CityService.AddCity(city);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]CityVM city)
        {
            await this.CityService.EditCity(city);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.CityService.DeleteCity(id);
            return Ok();
        }
    }
}
