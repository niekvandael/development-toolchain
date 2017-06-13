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
        private WorkplaceRepository _workplaceRepository;

        public ChecklistItemRepository(PreventionAdvisorDbContext context)
        {
            this._context = context;
            this._sessionTasks = new SessionTasks();
            this._workplaceRepository = new WorkplaceRepository(context);
        }

        public int GetCount(HttpContext httpContext)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);
            int total = 0;

            ICollection<Workplace> workplaces = this._workplaceRepository.Get(httpContext);
            foreach(Workplace workplace in workplaces){
                foreach(Category category in workplace.Categories){
                    total += category.ChecklistItems.Count;
                }
            }
            
            return total;
        }

        public int GetCountWithStatus(HttpContext httpContext, CheckListItemStatus checkListItemStatus)
        {
            var userId = this._sessionTasks.GetAppUserId(httpContext);
            int total = 0;

            ICollection<Workplace> workplaces = this._workplaceRepository.Get(httpContext);
            foreach(Workplace workplace in workplaces){
                foreach(Category category in workplace.Categories){
                    foreach(ChecklistItem checklistItem in category.ChecklistItems){
                        if(checklistItem.Status == (int) checkListItemStatus){
                            total++;
                        }
                    }
                }
            }
            
            return total;
        }
    }
}
