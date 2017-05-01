using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Models
{
    public class ChecklistItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public int Status { get; set; }

        [ForeignKey("WorkplaceId")]
        [JsonIgnore]
        public Workplace Workplace { get; set; }

        [ForeignKey("Workplace")]
        public Guid WorkplaceId { get; set; }


        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }
    }
}
