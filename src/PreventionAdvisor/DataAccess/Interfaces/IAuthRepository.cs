using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using PreventionAdvisor.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PreventionAdvisorDataAccess.Interfaces
{
    public interface IAuthRepository
    {
        System.Threading.Tasks.Task<bool> SignIn(HttpContext httpContext, UserViewModel vm);
        IdentityUser GetIdentityUser(HttpContext httpContext);
        AppUser GetAppUser(HttpContext httpContext);
        System.Threading.Tasks.Task SignOut(HttpContext httpContext);
        void SetSessionUser(HttpContext httpContext, AppUser user);
    }
}
