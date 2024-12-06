using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;

namespace ExpenseTrackerAPI.Mappers
{
    public static class BudgetMapper
    {
        public static BudgetDTO ToDTO(Budget budget)
        {
            if (budget == null) return null;

            return new BudgetDTO
            {
                BudgetID = budget.BudgetID,
                UserId = budget.UserId,
                Year = budget.Year,
                Month = budget.Month,
                TotalBudget = budget.TotalBudget,
                CreatedAt = budget.CreatedAt,
                UpdatedAt = budget.UpdatedAt
            };
        }

        public static Budget ToEntity(BudgetDTO budgetDTO)
        {
            if (budgetDTO == null) return null;

            return new Budget
            {
                BudgetID = budgetDTO.BudgetID,
                UserId = budgetDTO.UserId,
                Year = budgetDTO.Year,
                Month = budgetDTO.Month,
                TotalBudget = budgetDTO.TotalBudget,
                CreatedAt = budgetDTO.CreatedAt,
                UpdatedAt = budgetDTO.UpdatedAt
            };
        }
    }
}
