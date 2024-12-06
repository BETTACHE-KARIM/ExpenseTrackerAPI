namespace ExpenseTrackerAPI.Models.DTO
{
    public class NotificationDTO
    {
        public long NotificationID { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
