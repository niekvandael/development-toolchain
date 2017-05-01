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
    public class ChecklistItemController : Controller
    {
        private ChecklistItemRepository _checklistItemRepository;

        public ChecklistItemController(PreventionAdvisorDbContext dbContext)
        {
            this._checklistItemRepository = new ChecklistItemRepository(dbContext);
        }

        [HttpGet]
        public ObjectResult GetChecklistItems()
        {
            try
            {
                return Ok(this._checklistItemRepository.Get(HttpContext));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ObjectResult GetChecklistItem(Guid id)
        {
            try
            {
                return Ok(this._checklistItemRepository.Get(HttpContext, id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ObjectResult AddChecklistItem([FromBody] ChecklistItem checklistItem)
        {
            try
            {
                return Ok(this._checklistItemRepository.Create(HttpContext, checklistItem));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ObjectResult UpdateChecklistItem([FromBody] ChecklistItem checklistItem)
        {
            try
            {
                return Ok(this._checklistItemRepository.Update(HttpContext, checklistItem));

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public ObjectResult DeleteChecklistItem([FromBody] Guid id)
        {
            try
            {
                this._checklistItemRepository.Delete(HttpContext, id);
                return Ok(null);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
