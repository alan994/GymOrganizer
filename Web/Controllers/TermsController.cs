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
    [Route("api/terms")]
    public class TermsController : AuthController
    {
        public readonly ILogger<TermsController> Logger;
        public readonly TermService TermService;

        public TermsController(ILogger<TermsController> logger, TermService termService, GymOrganizerContext db) : base(db)
        {
            this.Logger = logger;
            this.TermService = termService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<TermVM> resultList = await this.TermService.GetAllTerms();
            return Ok(resultList);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            List<TermVM> resultList = await this.TermService.GetAllActiveTerms();
            return Ok(resultList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            TermVM term = await this.TermService.GetTermById(id);
            return Json(term);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]TermVM term)
        {
            await this.TermService.AddTerm(term);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]TermVM term)
        {
            await this.TermService.EditTerm(term);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.TermService.DeleteTerm(id);
            return Ok();
        }
    }
}
