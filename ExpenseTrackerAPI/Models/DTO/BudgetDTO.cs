namespace ExpenseTrackerAPI.Models.DTO
{
    public class BudgetDTO
    {
        public long BudgetID { get; set; }
        public string UserId { get; set; } 
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalBudget { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
