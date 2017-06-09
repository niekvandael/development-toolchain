﻿using Microsoft.AspNetCore.Http;
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
                .Where(w => w.Organization.UserId == userId)
                .Include(w => w.Address)
                .Include(w => w.Organization)
                .Include(w => w.Organization.Address)
                .Where(w => w.Title != "default")
                .ToList();
        }

        public ICollection<Workplace> GetIncompleteWorkplaces(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                .Where(w => w.Organization.UserId == userId)
                .Include(w => w.Address)
                .Include(w => w.Organization)
                .Include(w => w.Organization.Address)
                .Where(w => w.ChecklistItems.Any(c => c.Status != (int) CheckListItemStatus.OK))
                .Where(w => w.Title != "default")
                .ToList();
        }
      
        public ICollection<Workplace> GetCompleteWorkplaces(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                .Where(w => w.Organization.UserId == userId)
                .Include(w => w.Address)
                .Include(w => w.Organization)
                .Include(w => w.Organization.Address)
                .Where(w => w.ChecklistItems.All(c => c.Status == (int) CheckListItemStatus.OK))
                .Where(w => w.Title != "default")
                .ToList();
        }

        public Workplace Get(HttpContext httpContext, Guid id)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                            .Where(w => w.Organization.UserId == userId)
                            .Where(w => w.Id == id)
                            .Include(w => w.Address)
                            .Include(w => w.Organization)
                            .Include(w => w.Organization.Address)
                            .Include(w => w.ChecklistItems).ThenInclude(cli => cli.Category)
                            .FirstOrDefault();
        }

        public Workplace GetWorkplaceByName(HttpContext httpContext, string name)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Workplaces
                            .Where(w => w.Organization.UserId == userId)
                            .Where(w => w.Title == name)
                            .Include(w => w.Address)
                            .Include(w => w.Organization)
                            .Include(w => w.Organization.Address)
                            .Include(w => w.ChecklistItems).ThenInclude(cli => cli.Category)
                            .FirstOrDefault();
        }

        public Workplace Create(HttpContext httpContext, Workplace workplace)
        {
            try
            {
                setForeignKeysById(workplace);
                workplace.Organization.UserId = this._sessionTasks.GetAppUserId(httpContext);
                
                Workplace defaultWorkplace = this.GetWorkplaceByName(httpContext, "default");
                workplace.ChecklistItems = new List<ChecklistItem>();
                foreach(ChecklistItem defaultChecklistItem in defaultWorkplace.ChecklistItems){
                    workplace.ChecklistItems.Add(
                        new ChecklistItem(){
                            CategoryId = defaultChecklistItem.CategoryId,
                            Description = defaultChecklistItem.Description,
                            Status = defaultChecklistItem.Status,
                            Title = defaultChecklistItem.Title,
                            UserId = workplace.Organization.UserId
                        }
                    );
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
