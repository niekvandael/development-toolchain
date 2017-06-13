using PreventionAdvisor.Enums;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using PreventionAdvisor.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using Microsoft.EntityFrameworkCore;
using PreventionAdvisor.Config;
using PreventionAdvisorDataAccess.Repositories;

namespace GreenLiving.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private WorkplaceRepository _WorkplaceRepository;
        private ChecklistItemRepository _ChecklistItemRepository;

        public DashboardController(PreventionAdvisorDbContext dbContext)
        {
            this._WorkplaceRepository = new WorkplaceRepository(dbContext);
            this._ChecklistItemRepository = new ChecklistItemRepository(dbContext);
        }

        [HttpGet]
        public ObjectResult GetDashboard()
        {
            try
            {
                DashboardModel dashboardModel = new DashboardModel();
                
                // Incomplete workplaces
                dashboardModel.IncompleteWorkplaces = this._WorkplaceRepository.GetIncompleteWorkplaces(HttpContext);

                // Complete workplaces
                var completeWorkplaces = this._WorkplaceRepository.GetCompleteWorkplaces(HttpContext);

                // All workplaces
                var workplaces = this._WorkplaceRepository.Get(HttpContext);

                dashboardModel.ReportsCount = workplaces.Count;
                dashboardModel.TotalItemsFail = this._ChecklistItemRepository.GetCountWithStatus(HttpContext, CheckListItemStatus.NOT_OK);
                dashboardModel.TotalItemsOk = this._ChecklistItemRepository.GetCountWithStatus(HttpContext, CheckListItemStatus.OK);
                dashboardModel.TotalItemsNvt = this._ChecklistItemRepository.GetCountWithStatus(HttpContext, CheckListItemStatus.NVT);
                dashboardModel.TotalItemsNull = this._ChecklistItemRepository.GetCountWithStatus(HttpContext, CheckListItemStatus.NOT_FILLED_IN);

                dashboardModel.TotalItems = this._ChecklistItemRepository.GetCount(HttpContext);

                return Ok(dashboardModel);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
