using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreventionAdvisor.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public String Street { get; set; }
        public String Number { get; set; }
        public String Zipcode { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
    }
}