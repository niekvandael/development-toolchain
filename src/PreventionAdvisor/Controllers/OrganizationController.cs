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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreenLiving.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrganizationController : Controller
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly PreventionAdvisorDbContext _dbContext;

        public OrganizationController(HtmlEncoder htmlEncoder, PreventionAdvisorDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        [HttpGet]
        public ObjectResult GetOrganizations()
        {
            try
            {
                return Ok(this._dbContext.Organizations.ToList());
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
                return Ok(this._dbContext.Organizations.Include(o => o.Address).Where(o => o.Id == id).First());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ObjectResult AddOrganization([FromBody] Organization org)
        {
            try
            {
                this._dbContext.Organizations.Add(org);
                this._dbContext.SaveChanges();

                return Ok(org);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ObjectResult UpdateOrganization([FromBody] Organization org)
        {
            try
            {
                this._dbContext.Organizations.Update(org);
                this._dbContext.SaveChanges();

                return Ok(org);
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
                return Ok(this._dbContext.Organizations.Remove(this._dbContext.Organizations.Find(id)));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
