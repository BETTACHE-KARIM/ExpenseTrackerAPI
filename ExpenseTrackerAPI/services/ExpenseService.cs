using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Mappers;
using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;
using ExpenseTrackerAPI.services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public ExpenseService(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpensesAsync()
        {
            var expenses = await _context.Expenses
                .Include(e => e.User)
                .Include(e => e.Category)
                .ToListAsync();

            return expenses.Select(ExpenseMapper.ToDTO);
        }

        public async Task<ExpenseDTO> GetExpenseAsync(long id)
        {
            var expense = await _context.Expenses
                .Include(e => e.User)
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.ExpenseID == id);

            return expense == null ? null : ExpenseMapper.ToDTO(expense);
        }

        public async Task<ExpenseDTO> CreateExpenseAsync(ExpenseDTO expenseDTO)
        {
            var expense = ExpenseMapper.ToEntity(expenseDTO);
            expense.CreatedAt = DateTime.UtcNow;
            expense.UpdatedAt = DateTime.UtcNow;

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

        
            await CheckAndNotifyIfBudgetExceededAsync(expense.UserId, expense.CreatedAt);

            return ExpenseMapper.ToDTO(expense);
        }

        private async Task CheckAndNotifyIfBudgetExceededAsync(string userId, DateTime? expenseDate)
        {
            if (!expenseDate.HasValue)
            {
                throw new ArgumentException("Expense date cannot be null.", nameof(expenseDate));
            }

       
            int currentYear = expenseDate.Value.Year;
            int currentMonth = expenseDate.Value.Month;

            var totalExpenses = await _context.Expenses
                .Where(e => e.UserId == userId && e.CreatedAt.HasValue &&
                            e.CreatedAt.Value.Year == currentYear && e.CreatedAt.Value.Month == currentMonth)
                .SumAsync(e => e.Amount);

      
            var budget = await _context.Budgets
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Year == currentYear && b.Month == currentMonth);

            if (budget != null && totalExpenses > budget.TotalBudget)
            {
          
                var notification = new NotificationDTO
                {
                    UserId = userId,
                    Message = $"Your total expenses for {expenseDate.Value.ToString("MMMM yyyy")} have exceeded your budget of {budget.TotalBudget:C}.",
                    Status = NotificationStatus.Sent.ToString(),
                    CreatedAt = DateTime.UtcNow
                };

                await _notificationService.CreateNotificationAsync(notification);
            }
        }


        public async Task UpdateExpenseAsync(long id, ExpenseDTO expenseDTO)
        {
            if (id != expenseDTO.ExpenseID)
                throw new ArgumentException("Expense ID mismatch");

            var expense = ExpenseMapper.ToEntity(expenseDTO);
            expense.UpdatedAt = DateTime.UtcNow;

            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(long id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> GetTotalExpensesAsync(string userId)
        {
            return await _context.Expenses.Where(e => e.UserId == userId).SumAsync(e => e.Amount);
        }
    }

}
