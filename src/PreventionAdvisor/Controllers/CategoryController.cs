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
    public class CategoryController : Controller
    {
        private CategoryRepository _categoryRepository;

        public CategoryController(PreventionAdvisorDbContext dbContext)
        {
            this._categoryRepository = new CategoryRepository(dbContext);
        }

        [HttpGet]
        public ObjectResult GetCategorys()
        {
            try
            {
                return Ok(this._categoryRepository.Get(HttpContext));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ObjectResult GetCategory(Guid id)
        {
            try
            {
                return Ok(this._categoryRepository.Get(HttpContext, id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ObjectResult AddCategory([FromBody] Category category)
        {
            try
            {
                return Ok(this._categoryRepository.Create(HttpContext, category));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ObjectResult UpdateCategory([FromBody] Category category)
        {
            try
            {
                return Ok(this._categoryRepository.Update(HttpContext, category));

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public ObjectResult DeleteCategory([FromBody] Guid id)
        {
            try
            {
                this._categoryRepository.Delete(HttpContext, id);
                return Ok(null);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
