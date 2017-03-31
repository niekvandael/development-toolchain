using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
