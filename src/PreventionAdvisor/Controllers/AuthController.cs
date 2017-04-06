using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PreventionAdvisor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PreventionAdvisor.Controllers
{
 //   [ValidateAntiForgeryToken]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private SignInManager<User> _signInManager;

        public AuthController(SignInManager<User> signInManager)
        {
            this._signInManager = signInManager;
        }

        [HttpPost("/api/login")]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl) {
            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);

            if (result.Succeeded)
            {
                return Ok();
            }


            return BadRequest("Failed to login");
        }

        [HttpPost("/api/logout")]
        public async Task<ActionResult> Logout() {
            if (User.Identity.IsAuthenticated) {
                await _signInManager.SignOutAsync();
            }
            return Ok();
        }

    }
}
