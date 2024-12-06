using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpenseTrackerAPI.Models.Entities
{
    public class User: IdentityUser
    {
    
        

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

     

        public ICollection<Expense> Expenses { get; set; }
        [JsonIgnore]
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }


}
