using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PreventionAdvisor.Models
{
    public class AppUser 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        public String IdentityUserId { get; set; }
        [JsonIgnore]
        public virtual IdentityUser IdentityUser { get; set; }

        public String Firstname { get; set; }
        public String Lastname { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }

    }
}
