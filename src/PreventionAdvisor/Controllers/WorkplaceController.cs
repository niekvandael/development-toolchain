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
using PreventionAdvisorDataAccess.Common;
using PreventionAdvisor.Enums;
using PreventionAdvisor;

namespace GreenLiving.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkplaceController : Controller
    {
        private WorkplaceRepository _workplaceRepository;
        private SessionTasks _sessionTasks;

        public WorkplaceController(PreventionAdvisorDbContext dbContext)
        {
            this._workplaceRepository = new WorkplaceRepository(dbContext);
            this._sessionTasks = new SessionTasks();
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
                Workplace workplace;

                try {
                    Guid.Parse(id);
                    // GUID Provided
                    workplace = this._workplaceRepository.Get(HttpContext, Guid.Parse(id));
                }
                catch(System.Exception){
                    // Not a GUID
                    if(id.Equals("default"))
                    {
                        workplace = _workplaceRepository.GetWorkplaceByName(HttpContext, "default");
                        if(workplace == null){
                            this.CreateDefaultWorkplace();
                            workplace = _workplaceRepository.GetWorkplaceByName(HttpContext, "default");
                        }
                    } else 
                    {
                        return Ok(this._workplaceRepository.Get(HttpContext, Guid.Parse(id)));
                    }
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

        private void CreateDefaultWorkplace(){
           Guid UserId = this._sessionTasks.GetAppUserId(HttpContext);
           Workplace defaultWorkplace = DbInitializer.getDefaultWorkplace();
           defaultWorkplace.User.Id = UserId;
           _workplaceRepository.Create(HttpContext, defaultWorkplace);
        }
    }
}
