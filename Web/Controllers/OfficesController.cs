using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Web.ViewModels;
using Microsoft.Extensions.Logging;
using Web.Services;
using System.Collections.Generic;
using Web.Utils;

namespace Web.Controllers
{
    [Route("api/offices")]
    public class OfficesController : Controller
    {
        public readonly ILogger<OfficesController> logger;
        public readonly OfficeService officeService;

        public OfficesController(ILogger<OfficesController> logger, OfficeService officeService)
        {
            this.logger = logger;
            this.officeService = officeService;
        }

        [HttpGet]
        [ServiceFilter(typeof(UserDataActionFilter))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<OfficeVM> resultList = await officeService.GetAllOfficessAsync();
                return Ok(resultList);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to fetch offices", ex);
                return BadRequest();
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok();
        }
                
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]OfficeVM office)
        {
            return Ok();
        }

        
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]OfficeVM office)
        {
            return Ok();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }
    }
}
