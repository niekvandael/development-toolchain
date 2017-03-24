using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Salt { get; set; }
        public String Phone { get; set; }
        public Address Address { get; set; }
        public IList<Organization> Organizations { get; set; }
    }
}
