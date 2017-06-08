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
    public class WorkplaceController : Controller
    {
        private WorkplaceRepository _workplaceRepository;

        public WorkplaceController(PreventionAdvisorDbContext dbContext)
        {
            this._workplaceRepository = new WorkplaceRepository(dbContext);
        }

        [HttpGet]
        public ObjectResult GetWorkplaces()
        {
            try
            {
                return Ok(this._workplaceRepository.Get(HttpContext));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ObjectResult GetWorkplace(String id)
        {
            try
            {
                Workplace workplace = this._workplaceRepository.Get(HttpContext, Guid.Parse(id));
                if(id.Equals("default") && workplace == null)
                {
                    workplace = createDefaultWorkplace();
                } else 
                {
                    return Ok(this._workplaceRepository.Get(HttpContext, Guid.Parse(id)));
                }

                return Ok(workplace);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ObjectResult AddWorkplace([FromBody] Workplace workplace)
        {
            try
            {
                return Ok(this._workplaceRepository.Create(HttpContext, workplace));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ObjectResult UpdateWorkplace([FromBody] Workplace workplace)
        {
            try
            {
                return Ok(this._workplaceRepository.Update(HttpContext, workplace));

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public ObjectResult DeleteWorkplace([FromBody] Guid id)
        {
            try
            {
                this._workplaceRepository.Delete(HttpContext, id);
                return Ok(null);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private Workplace createDefaultWorkplace(){
            Workplace workplace = new Workplace();
            workplace.Title = "default";
            workplace.Organization = new Organization();
            workplace.Organization.Name = "default";
            workplace.ChecklistItems.Add(
                new ChecklistItem(){
                Category = new Category(){ Title = "Category 1"},
                Description = "Item 1",
                Status = 2}
            );
            workplace.ChecklistItems.Add(
                new ChecklistItem(){
                Category = new Category(){ Title = "Category 2"},
                Description = "Item 1",
                Status = 2}
            );

            _workplaceRepository.Create(HttpContext, workplace);
            return this._workplaceRepository.GetWorkplaceByName(HttpContext, "default");
        }
    }
}
