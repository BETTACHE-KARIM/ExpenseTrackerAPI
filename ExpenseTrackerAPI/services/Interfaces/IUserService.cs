using ExpenseTrackerAPI.Models.DTO;

namespace ExpenseTrackerAPI.services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetUsersAsync();
        Task<UserDTO?> GetUserAsync(string id);
        Task<bool> DeleteUserAsync(string id);
    }
}
