using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using PreventionAdvisor.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisorDataAccess.Repositories;

namespace GreenLiving.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private OrganizationRepository _organizationRepository;

        public OrganizationController(PreventionAdvisorDbContext dbContext)
        {
            this._organizationRepository = new OrganizationRepository(dbContext);
        }

        [HttpGet]
        public ObjectResult GetOrganizations()
        {
            try
            {
                return Ok(this._organizationRepository.Get(HttpContext));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ObjectResult GetOrganization(Guid id)
        {
            try
            {
                return Ok(this._organizationRepository.Get(HttpContext, id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ObjectResult AddOrganization([FromBody] Organization organization)
        {
            try
            {
                return Ok(this._organizationRepository.Create(HttpContext, organization));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ObjectResult UpdateOrganization([FromBody] Organization organization)
        {
            try
            {
                return Ok(this._organizationRepository.Update(HttpContext, organization));

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public ObjectResult DeleteOrganization([FromBody] Guid id)
        {
            try
            {
                this._organizationRepository.Delete(HttpContext, id);
                return Ok(null);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
