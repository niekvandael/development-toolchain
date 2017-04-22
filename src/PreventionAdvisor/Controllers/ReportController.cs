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
using PreventionAdvisor.Config;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreenLiving.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly PreventionAdvisorDbContext _dbContext;

        public ReportController(HtmlEncoder htmlEncoder, PreventionAdvisorDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        // GET: api/values
        [HttpPost]
        public ObjectResult DownloadReport()
        {
            try
            {
                return Ok(_dbContext.Users.Find(this.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}")]
        public string DownloadReport(string id)
        {
            return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            /*
                        var userName = this.User.Identity.Name; // this is the username

                                    var url = @"http://localhost:3000/api/generatePDF/" + id; 

                        var url = @"http://greenlivingpdf.mybluemix.net/api/generatePDF/" + id;
                        return Redirect(url);
             */
        }

    }
}
