using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;

namespace ExpenseTrackerAPI.Mappers
{
    public static class ExpenseMapper
    {
        public static ExpenseDTO ToDTO(Expense expense)
        {
            return new ExpenseDTO
            {
                ExpenseID = expense.ExpenseID,
                UserId = expense.UserId,
              
                CategoryID = expense.CategoryID,
         
                Amount = expense.Amount,
                Description = expense.Description,
                CreatedAt = expense.CreatedAt,
                UpdatedAt = expense.UpdatedAt
            };
        }

        public static Expense ToEntity(ExpenseDTO dto)
        {
            return new Expense
            {
                ExpenseID = dto.ExpenseID,
                UserId = dto.UserId,
                CategoryID = dto.CategoryID,
                Amount = dto.Amount,
                Description = dto.Description,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }
    }
}
