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

        public DashboardController(PreventionAdvisorDbContext dbContext)
        {
            this._WorkplaceRepository = new WorkplaceRepository(dbContext);
        }

        [HttpGet]
        public ObjectResult GetDashboard()
        {
            try
            {
                DashboardModel dashboardModel = new DashboardModel();
                
                // Incomplete
                dashboardModel.IncompleteWorkplaces = this._WorkplaceRepository.GetIncompleteWorkplaces(HttpContext);

                // Complete
                var completeWorkplaces = this._WorkplaceRepository.GetCompleteWorkplaces(HttpContext);

                // All
                var workplaces = this._WorkplaceRepository.Get(HttpContext);

                dashboardModel.ReportsCount = workplaces.Count;
                dashboardModel.TotalItemsFail = dashboardModel.IncompleteWorkplaces.Count;
                dashboardModel.TotalItemsOk = completeWorkplaces.Count;
                dashboardModel.TotalItems = workplaces.Count;

                return Ok(dashboardModel);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
