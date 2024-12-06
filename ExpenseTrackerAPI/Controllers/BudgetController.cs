using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;
using ExpenseTrackerAPI.services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBudgets()
        {
            var budgets = await _budgetService.GetAllBudgetsAsync();
            return Ok(budgets);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetBudget(long id)
        {
            var budget = await _budgetService.GetBudgetByIdAsync(id);
            return budget == null ? NotFound() : Ok(budget);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBudgetsByUserId(string userId)
        {
            var budgets = await _budgetService.GetBudgetsByUserIdAsync(userId);
            return Ok(budgets);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget(BudgetDTO budgetDTO)
        {
            var createdBudget = await _budgetService.CreateBudgetAsync(budgetDTO);
            return CreatedAtAction(nameof(GetBudget), new { id = createdBudget.BudgetID }, createdBudget);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateBudget(long id, BudgetDTO budgetDTO)
        {
            var updatedBudget = await _budgetService.UpdateBudgetAsync(id, budgetDTO);
            return updatedBudget == null ? NotFound() : NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteBudget(long id)
        {
            var success = await _budgetService.DeleteBudgetAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
