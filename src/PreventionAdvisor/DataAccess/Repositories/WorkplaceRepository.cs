using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using PreventionAdvisorDataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PreventionAdvisor.Enums;

namespace PreventionAdvisorDataAccess.Repositories
{
    public class WorkplaceRepository
    {
        private PreventionAdvisorDbContext _context;
        private SessionTasks _sessionTasks;

        public WorkplaceRepository(PreventionAdvisorDbContext context)
        {
            this._context = context;
            this._sessionTasks = new SessionTasks();
        }

        public ICollection<Workplace> Get(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                .Include(w => w.Address)
                .Include(w => w.Organization)
                .Include(w => w.Organization.Address)
                .Where(w => w.Title != "default")
                .Where(w => w.Organization.UserId == userId)
                .ToList();
        }

        public ICollection<Workplace> GetIncompleteWorkplaces(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                .Include(w => w.Address)
                .Include(w => w.Organization)
                .Include(w => w.Organization.Address)
                .Include(w => w.Categories).ThenInclude(c => c.ChecklistItems)
                .Where(w => w.Categories.Any(c => c.ChecklistItems.Any(cli => cli.Status != (int) CheckListItemStatus.OK)))
                .Where(w => w.Title != "default")
                .Where(w => w.Organization.UserId == userId)
                .ToList();
        }
      
        public ICollection<Workplace> GetCompleteWorkplaces(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                .Include(w => w.Address)
                .Include(w => w.Organization)
                .Include(w => w.Organization.Address)
                .Include(w => w.Categories).ThenInclude(c => c.ChecklistItems)
                .Where(w => w.Categories.Any(c => c.ChecklistItems.All(cli => cli.Status == (int) CheckListItemStatus.OK)))
                .Where(w => w.Title != "default")
                .Where(w => w.Organization.UserId == userId)
                .ToList();
        }

        public Workplace Get(HttpContext httpContext, Guid id)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                            .Include(w => w.Address)
                            .Include(w => w.Organization)
                            .Include(w => w.Organization.Address)
                            .Include(w => w.Categories).ThenInclude(c => c.ChecklistItems)
                            .Where(w => w.Organization.UserId == userId)
                            .Where(w => w.Id == id)
                            .FirstOrDefault();
        }

        public Workplace GetWorkplaceByName(HttpContext httpContext, string name)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                            .Include(w => w.Address)
                            .Include(w => w.Organization)
                            .Include(w => w.Organization.Address)
                            .Include(w => w.Categories).ThenInclude(c => c.ChecklistItems)
                            .Where(w => w.Organization.UserId == userId)
                            .Where(w => w.Title == name)
                            .FirstOrDefault();
        }

        public Workplace Create(HttpContext httpContext, Workplace workplace)
        {
            try
            {
                setForeignKeysById(workplace);
                workplace.UserId = this._sessionTasks.GetAppUserId(httpContext);
                
                Workplace defaultWorkplace = this.GetWorkplaceByName(httpContext, "default");
                workplace.Categories = new List<Category>();
                foreach(Category category in defaultWorkplace.Categories){
                    workplace.Categories.Add(category.getClone());
                }

                _context.Add(workplace);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return workplace;
        }

        public Workplace Update(HttpContext httpContext, Workplace workplace)
        {
            try
            {
                this.setForeignKeysById(workplace);
                workplace.Organization.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Update(workplace);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return workplace;
        }

        public void Delete(HttpContext httpContext, Guid id)
        {
            try
            {
                Workplace workplace = this.Get(httpContext, id);
                workplace.Organization.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Remove(workplace);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void setForeignKeysById(Workplace workplace)
        {
            if (workplace.Organization.Id != workplace.OrganizationId) {
                Organization org = _context.Organizations.Find(workplace.OrganizationId);
                if (org != null) {
                    workplace.Organization = org;
                }
            }
        }


    }
}
