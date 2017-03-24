using System;
using System.ComponentModel.DataAnnotations;

namespace PreventionAdvisor.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public User User { get; set; }
    }
}