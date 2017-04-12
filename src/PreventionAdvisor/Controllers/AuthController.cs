using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PreventionAdvisor.Models;
using PreventionAdvisor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace PreventionAdvisor.Controllers
{

    //   [ValidateAntiForgeryToken]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private readonly PreventionAdvisorDbContext _dbContext;

        public AuthController(SignInManager<IdentityUser> signInManager, PreventionAdvisorDbContext dbContext = null)
        {
            _dbContext = dbContext;
            this._signInManager = signInManager;
        }

        [HttpPost("/api/login")]
        public async Task<ActionResult> Login(UserViewModel vm, string returnUrl) {
            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);

            if (result.Succeeded)
            {

                IdentityUser usr = this._dbContext.Users.Where(u => u.UserName == vm.Username).First() ;

                return Ok(Mapper.Map<UserViewModel>(usr));
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
