using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Salt { get; set; }
        public String Phone { get; set; }
        public Address Address { get; set; }
        public ICollection<Organization> Organizations { get; set; }
    }
}
