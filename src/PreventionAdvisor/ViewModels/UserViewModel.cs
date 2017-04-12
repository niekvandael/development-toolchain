using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public String Firstname { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Username { get; set; }

        [JsonIgnore]
        public String Password { get; set; }
    }
}
