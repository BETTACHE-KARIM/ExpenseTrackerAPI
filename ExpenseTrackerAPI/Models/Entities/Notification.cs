using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerAPI.Models.Entities
{
    public class Notification
    {
        [Key]
        public long NotificationID { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        [StringLength(255)]
        public string Message { get; set; }

        [Required]
        public NotificationStatus Status { get; set; }

    
        public DateTime? CreatedAt { get; set; }
    }
    public enum NotificationStatus
    {
        Pending,
        Sent,
        Read
    }


    

}
