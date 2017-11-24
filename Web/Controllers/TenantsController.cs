using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web.ViewModels;
using Web.Services;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("api/tenants")]
    [Authorize]
    public class TenantsController : AuthController
    {
        public readonly TenantService TenantService;
        public readonly ILogger<TenantsController> Logger;

        public TenantsController(GymOrganizerContext db, TenantService tenantService, ILogger<TenantsController> logger) : base(db)
        {
            this.TenantService = tenantService;
            this.Logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<TenantVM> resultList = await this.TenantService.GetAllTenants();
            return Ok(resultList);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActive()
        {
            List<TenantVM> resultList = await this.TenantService.GetAllActiveTenants();
            return Ok(resultList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            TenantVM tenant = await this.TenantService.GetTenantById(id);
            return Json(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]TenantVM tenant)
        {
            await this.TenantService.AddTenant(tenant);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]TenantVM tenant)
        {
            await this.TenantService.EditTenant(tenant);
            return Ok();
        }

    }
}
