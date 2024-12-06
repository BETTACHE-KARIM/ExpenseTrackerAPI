using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;

namespace ExpenseTrackerAPI.services.Interfaces
{

    public interface IBudgetService
    {
        Task<IEnumerable<BudgetDTO>> GetAllBudgetsAsync();
        Task<BudgetDTO?> GetBudgetByIdAsync(long id);
        Task<IEnumerable<BudgetDTO>> GetBudgetsByUserIdAsync(string userId);
        Task<BudgetDTO> CreateBudgetAsync(BudgetDTO budgetDTO);
        Task<BudgetDTO?> UpdateBudgetAsync(long id, BudgetDTO budgetDTO);
        Task<bool> DeleteBudgetAsync(long id);
    }
}
