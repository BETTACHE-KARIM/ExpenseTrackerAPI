using ExpenseTrackerAPI.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTrackerAPI.services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDTO model);
        Task<string?> LoginUserAsync(LoginDTO model);
    }
}
