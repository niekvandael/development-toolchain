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
    public class CategoryRepository
    {
        private PreventionAdvisorDbContext _context;
        private SessionTasks _sessionTasks;

        public CategoryRepository(PreventionAdvisorDbContext context)
        {
            this._context = context;
            this._sessionTasks = new SessionTasks();
        }

        public ICollection<Category> Get(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Categories
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public Category Get(HttpContext httpContext, Guid id)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);

            return this._context.Categories
                 .Where(c => c.UserId == userId)
                 .Where(c => c.Id == id)
                 .FirstOrDefault();
        }

        public Category Create(HttpContext httpContext, Category category)
        {
            try
            {
                category.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Add(category);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return category;
        }

        public Category Update(HttpContext httpContext, Category category)
        {
            try
            {
                category.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Update(category);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return category;
        }

        public void Delete(HttpContext httpContext, Guid id)
        {
            try
            {
                Category category = this.Get(httpContext, id);
                category.UserId = this._sessionTasks.GetAppUserId(httpContext);

                _context.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
