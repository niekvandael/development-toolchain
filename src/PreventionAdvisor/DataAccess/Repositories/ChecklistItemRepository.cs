using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisor.Models;
using PreventionAdvisorDataAccess.Common;
using System;
using System.Collections.Generic;
using PreventionAdvisor.Enums;
using System.Linq;
using System.Text;

namespace PreventionAdvisorDataAccess.Repositories
{
    public class ChecklistItemRepository
    {
        private PreventionAdvisorDbContext _context;
        private SessionTasks _sessionTasks;

        public ChecklistItemRepository(PreventionAdvisorDbContext context)
        {
            this._context = context;
            this._sessionTasks = new SessionTasks();
        }

        public ICollection<ChecklistItem> Get(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.ChecklistItems
                .Where(c => c.UserId == userId)
                .ToList();
        }
        
        public int GetCountWithStatus(HttpContext httpContext, CheckListItemStatus checkListItemStatus)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.ChecklistItems
                .Count(c => c.UserId == userId && c.Status == (int) checkListItemStatus);
        }

        public ChecklistItem Get(HttpContext httpContext, Guid id)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.ChecklistItems
                 .Where(c => c.UserId == userId)
                 .Where(c => c.Id == id)
                 .FirstOrDefault();
        }

        public ChecklistItem Create(HttpContext httpContext, ChecklistItem checklistItem)
        {
            try
            {
                this.setForeignKeysById(checklistItem);

                checklistItem.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Add(checklistItem);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return checklistItem;
        }

        public ChecklistItem Update(HttpContext httpContext, ChecklistItem checklistItem)
        {
            try
            {
                this.setForeignKeysById(checklistItem);
                checklistItem.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Update(checklistItem);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return checklistItem;
        }

        public void Delete(HttpContext httpContext, Guid id)
        {
            try
            {
                ChecklistItem checklistItem = this.Get(httpContext, id);
                checklistItem.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Remove(checklistItem);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void setForeignKeysById(ChecklistItem checklistItem)
        {
            if (checklistItem.Category.Id != checklistItem.CategoryId)
            {
                Category cat = _context.Categories.Find(checklistItem.CategoryId);
                if (cat != null)
                {
                    checklistItem.Category = cat;
                }
            }

            if (checklistItem.Workplace != null && checklistItem.Workplace.Id != checklistItem.WorkplaceId)
            {
                Workplace workplace = _context.Workplaces.Find(checklistItem.WorkplaceId);
                if (workplace != null)
                {
                    checklistItem.Workplace = workplace;
                }
            }
        }
    }
}
