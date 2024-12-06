namespace ExpenseTrackerAPI.Models.DTO
{
    public class ExpenseDTO
    {
        public long ExpenseID { get; set; }
        public string UserId { get; set; }

        public int CategoryID { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
