using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Models
{
    public class DashboardModel
    {
       public ICollection<Workplace> IncompleteWorkplaces { get; set; }
       public int ReportsCount { get; set; }
       public int TotalItems { get; set; }
       public int TotalItemsOk { get; set; }
       public int TotalItemsFail { get; set; }
       public int TotalItemsNvt { get; set; }
       public int TotalItemsNull { get; set; }

    }
}
