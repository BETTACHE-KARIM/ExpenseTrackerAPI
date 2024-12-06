using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var expenses = await _expenseService.GetExpensesAsync();
            return Ok(expenses);
        }

        // GET: api/Expense/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(long id)
        {
            var expense = await _expenseService.GetExpenseAsync(id);
            if (expense == null) return NotFound();
            return Ok(expense);
        }

        // POST: api/Expense
        [HttpPost]
        public async Task<IActionResult> CreateExpense(ExpenseDTO expenseDTO)
        {
            if (expenseDTO == null)
                return BadRequest("Expense data is required.");

            var createdExpense = await _expenseService.CreateExpenseAsync(expenseDTO);
            return CreatedAtAction(nameof(GetExpense), new { id = createdExpense.ExpenseID }, createdExpense);
        }

        // PUT: api/Expense/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(long id, ExpenseDTO expenseDTO)
        {
            if (id != expenseDTO.ExpenseID)
                return BadRequest("Expense ID mismatch.");

            try
            {
                await _expenseService.UpdateExpenseAsync(id, expenseDTO);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        // DELETE: api/Expense/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(long id)
        {
            var existingExpense = await _expenseService.GetExpenseAsync(id);
            if (existingExpense == null)
                return NotFound();

            await _expenseService.DeleteExpenseAsync(id);
            return NoContent();
        }

        // GET: api/Expense/total/{userId}
        [HttpGet("total/{userId}")]
        public async Task<IActionResult> GetTotalExpenses(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("User ID is required.");

            var totalExpenses = await _expenseService.GetTotalExpensesAsync(userId);
            return Ok(new { TotalExpenses = totalExpenses });
        }
    }
}
