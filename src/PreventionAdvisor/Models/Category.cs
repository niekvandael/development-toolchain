using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreventionAdvisor.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public String Title { get; set; }

        public Guid UserId { get; set; }
        [JsonIgnore]
        public virtual AppUser User { get; set; }
    }
}