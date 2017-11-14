using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Web.ViewModels;
using Microsoft.Extensions.Logging;
using Web.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Data.Db;

namespace Web.Controllers
{
    [Route("api/offices")]
    [Authorize]
    public class OfficesController : AuthController
    {
        public readonly ILogger<OfficesController> Logger;
        public readonly OfficeService OfficeService;

        public OfficesController(ILogger<OfficesController> logger, OfficeService officeService, GymOrganizerContext db) : base(db)
        {
            this.Logger = logger;
            this.OfficeService = officeService;
        }

        [HttpGet]        
        public async Task<IActionResult> GetAll()
        {
            List<OfficeVM> resultList = await this.OfficeService.GetAllOfficessAsync();
            return Ok(resultList);
        }


        [HttpGet("{id}")]        
        public async Task<IActionResult> GetById(Guid id)
        {
            OfficeVM office = await this.OfficeService.GetOfficeByIdAsync(id);
            return Json(office);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]OfficeVM office)
        {
            await this.OfficeService.AddOffice(office);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]OfficeVM office)
        {
            await this.OfficeService.EditOffice(office);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.OfficeService.DeleteOffice(id);
            return Ok();
        }
    }
}
