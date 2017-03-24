using System;
using System.ComponentModel.DataAnnotations;

namespace PreventionAdvisor.Models
{
    public class Organization
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Vat { get; set; }
        public String Website { get; set; }
        public String Phone { get; set; }
        public Address Address { get; set; }
    }
}