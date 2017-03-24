using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GetStartedDotnet.Models;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreenLiving.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly GreenLivingDbContext _dbContext;

        public ReportController(HtmlEncoder htmlEncoder, GreenLivingDbContext dbContext = null)
        {
            _dbContext = dbContext;
            _htmlEncoder = htmlEncoder;
        }

        // GET: api/values
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            //            var url = @"http://localhost:3000/api/generatePDF/" + id;  /* TEST -URL */

            var url = @"http://greenlivingpdf.mybluemix.net/api/generatePDF/" + id;
            return Redirect(url);
        }

    }
}
