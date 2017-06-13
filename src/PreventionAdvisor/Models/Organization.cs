using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PreventionAdvisor.Models
{
    public class Organization
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Vat { get; set; }
        public String Website { get; set; }
        public String Phone { get; set; }
        public Address Address { get; set; }
        public Guid UserId { get; set; }
        [JsonIgnore]
        public ICollection<Workplace> Workplaces { get; set; }
        public virtual AppUser User { get; set; }
    }
}