using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Models
{
    public class Workplace
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }

        [ForeignKey("Organization")]
        public Guid OrganizationId { get; set; }

        public String ProjectNumber { get; set; }
        public Address Address { get; set; }
        public String Title { get; set; }
        public String ProjectLead { get; set; }
        public String ProjectController { get; set; }
        public String Description { get; set; }

        public ICollection<ChecklistItem> ChecklistItems { get; set; }
    }

}
