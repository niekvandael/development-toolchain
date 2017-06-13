using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public ICollection<ChecklistItem> ChecklistItems { get; set; }

        public Category getClone(){
                Category category = new Category();
                category.ChecklistItems = new List<ChecklistItem>();
                category.Title = this.Title;

                foreach(ChecklistItem checklistItem in this.ChecklistItems)
                {
                    ChecklistItem newCheckListItem = new ChecklistItem(){
                        Description = checklistItem.Description,
                        Status = checklistItem.Status,
                        Title = checklistItem.Title
                    };

                    category.ChecklistItems.Add(newCheckListItem);
                }

                return category;
        }
    }
}