using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models.Entities
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

 
        public DateTime? CreatedAt { get; set; }


        public DateTime? UpdatedAt { get; set; }

     
        public ICollection<Expense>? Expenses { get; set; }
    }


}
