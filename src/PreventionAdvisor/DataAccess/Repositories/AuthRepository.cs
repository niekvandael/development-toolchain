using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using PreventionAdvisor.ViewModels;
using PreventionAdvisorDataAccess.Common;
using PreventionAdvisorDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PreventionAdvisorDataAccess.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private PreventionAdvisorDbContext _dbContext;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private SessionTasks _sessionTasks;

        public AuthRepository(PreventionAdvisorDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._sessionTasks = new SessionTasks();

        }

        public async System.Threading.Tasks.Task<bool> SignIn(HttpContext httpContext, UserViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, false, false);
            return result.Succeeded;
        }


        public IdentityUser GetIdentityUser(HttpContext httpContext)
        {
            return  _dbContext.Users.Where(u => u.UserName == httpContext.User.Identity.Name).First();
        }

        public AppUser GetAppUser(HttpContext httpContext)
        {

            IdentityUser identityUser = this.GetIdentityUser(httpContext);
            AppUser appUser = this._dbContext.AppUsers.Where(u => u.IdentityUserId == identityUser.Id).First();

            return appUser;
        }

        public async System.Threading.Tasks.Task SignOut(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public void SetSessionUser(HttpContext httpContext, AppUser user)
        {
            this._sessionTasks.SetAppUser(httpContext, user);
        }
    }
}
