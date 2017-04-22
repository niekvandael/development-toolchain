using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using PreventionAdvisor.ViewModels;
using PreventionAdvisorDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace PreventionAdvisor.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private AuthRepository _authRepository;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, PreventionAdvisorDbContext dbContext = null)
        {
            this._authRepository = new AuthRepository(dbContext, userManager, signInManager);

        }

        [HttpPost("/api/login")]
        public async Task<ActionResult> Login(UserViewModel vm, string returnUrl)
        {

            var succeeded = await this._authRepository.SignIn(HttpContext, vm);

            if (succeeded)
            {
                AppUser usr = _authRepository.GetAppUser(HttpContext);
                _authRepository.SetSessionUser(HttpContext, usr);

                return Ok(_authRepository.GetAppUser(HttpContext));
            }


            return BadRequest("Failed to login");

        }

        [HttpPost("/api/logout")]
        public async Task<ActionResult> Logout()
        {
            await this._authRepository.SignOut(HttpContext);
            return Ok();
        }

        [Authorize]
        [HttpGet("/api/whoami")]
        public ActionResult getAuthenticatedUser()
        {
            AppUser appUser = this._authRepository.GetAppUser(HttpContext);
            return Ok(appUser);
        }
    }
}
