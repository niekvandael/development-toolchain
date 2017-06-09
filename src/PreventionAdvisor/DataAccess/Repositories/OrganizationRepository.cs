using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using PreventionAdvisorDataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreventionAdvisorDataAccess.Repositories
{
    public class OrganizationRepository
    {
        private PreventionAdvisorDbContext _context;
        private SessionTasks _sessionTasks;

        public OrganizationRepository(PreventionAdvisorDbContext context)
        {
            this._context = context;
            this._sessionTasks = new SessionTasks();
        }

        public ICollection<Organization> Get(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Organizations
                .Where(o => o.UserId == userId)
                .Where(o => o.Name != "default")
                .Include(o => o.Address)
                .ToList();
        }

        public Organization Get(HttpContext httpContext, Guid id)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Organizations
                            .Where(o => o.UserId == userId)
                            .Where(o => o.Id == id)
                            .Include(o => o.Address)
                            .FirstOrDefault();
        }

        public Organization Create(HttpContext httpContext, Organization organization)
        {
            try
            {
                organization.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Add(organization);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return organization;
        }

        public Organization Update(HttpContext httpContext, Organization organization)
        {
            try
            {
                organization.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Update(organization);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return organization;
        }

        public void Delete(HttpContext httpContext, Guid id)
        {
            try
            {
                Organization organization = this.Get(httpContext, id);
                organization.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Remove(organization);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
