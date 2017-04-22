using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using PreventionAdvisorDataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PreventionAdvisorDataAccess.Common
{

    public class SessionTasks
    {
        const String SessionKeyUserId = "_userId";

        public void SetAppUser(HttpContext httpContext, AppUser user)
        {
            httpContext.Session.SetString(SessionKeyUserId, user.Id.ToString());
        }

        public Guid GetAppUserId(HttpContext httpContext)
        {
            return new Guid(httpContext.Session.GetString(SessionKeyUserId));
        }
    }
}
