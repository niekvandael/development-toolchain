using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Models
{
    public class Checklist
    {
        [Key]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Status { get; set; }
        public Workplace Workplace { get; set; }
        public Category Category { get; set; }
    }
}
