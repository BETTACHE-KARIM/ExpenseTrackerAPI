using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Mappers;
using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;
using ExpenseTrackerAPI.services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.services
{
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;

        public BudgetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BudgetDTO>> GetAllBudgetsAsync()
        {
            var budgets = await _context.Budgets.Include(b => b.User).ToListAsync();
            return budgets.Select(BudgetMapper.ToDTO);
        }

        public async Task<BudgetDTO?> GetBudgetByIdAsync(long id)
        {
            var budget = await _context.Budgets
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BudgetID == id);
            return BudgetMapper.ToDTO(budget);
        }

        public async Task<IEnumerable<BudgetDTO>> GetBudgetsByUserIdAsync(string userId)
        {
            var budgets = await _context.Budgets
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();
            return budgets.Select(BudgetMapper.ToDTO);
        }

        public async Task<BudgetDTO> CreateBudgetAsync(BudgetDTO budgetDTO)
        {
            var budget = BudgetMapper.ToEntity(budgetDTO);

            // Ensure the UserId exists
            var user = await _context.Users.FindAsync(budget.UserId);
            if (user == null)
                throw new ArgumentException("User not found.");

            budget.User = user;
            budget.CreatedAt = DateTime.UtcNow;
            budget.UpdatedAt = DateTime.UtcNow;

            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return BudgetMapper.ToDTO(budget);
        }

        public async Task<BudgetDTO?> UpdateBudgetAsync(long id, BudgetDTO budgetDTO)
        {
            var existingBudget = await _context.Budgets.FindAsync(id);
            if (existingBudget == null) return null;

            existingBudget.Year = budgetDTO.Year;
            existingBudget.Month = budgetDTO.Month;
            existingBudget.TotalBudget = budgetDTO.TotalBudget;
            existingBudget.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return BudgetMapper.ToDTO(existingBudget);
        }

        public async Task<bool> DeleteBudgetAsync(long id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null) return false;

        
            var userId = budget.UserId;
            var year = budget.Year;
            var month = budget.Month;

          
            var expensesToDelete = await _context.Expenses
                .Where(e => e.UserId == userId && e.CreatedAt.HasValue &&
                            e.CreatedAt.Value.Year == year && e.CreatedAt.Value.Month == month)
                .ToListAsync();

            _context.Expenses.RemoveRange(expensesToDelete);

       
            _context.Budgets.Remove(budget);

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
