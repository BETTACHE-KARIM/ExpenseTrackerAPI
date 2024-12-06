using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;

namespace ExpenseTrackerAPI.services.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDTO>> GetExpensesAsync();
        Task<ExpenseDTO> GetExpenseAsync(long id);
        Task<ExpenseDTO> CreateExpenseAsync(ExpenseDTO expenseDTO);
        Task UpdateExpenseAsync(long id, ExpenseDTO expenseDTO);
        Task DeleteExpenseAsync(long id);
        Task<decimal> GetTotalExpensesAsync(string userId);
    }

}
