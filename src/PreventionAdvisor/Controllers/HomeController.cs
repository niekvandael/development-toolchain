using Microsoft.AspNetCore.Mvc;

namespace PreventionAdvisor.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (!Request.IsHttps && !Request.Host.Value.Contains("localhost"))
            {
                string redirectUrl = Request.Host.ToString().Replace("http:", "https:");
                Response.Redirect(redirectUrl);
            }

            return View();
        }
    }
}
