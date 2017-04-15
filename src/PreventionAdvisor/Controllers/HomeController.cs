using Microsoft.AspNetCore.Mvc;

namespace PreventionAdvisor.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
