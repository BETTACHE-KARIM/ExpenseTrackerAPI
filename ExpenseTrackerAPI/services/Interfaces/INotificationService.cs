using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;

namespace ExpenseTrackerAPI.services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetNotificationsAsync();
        Task<NotificationDTO> GetNotificationAsync(long id);
        Task<NotificationDTO> CreateNotificationAsync(NotificationDTO notificationDTO);
        Task UpdateNotificationAsync(long id, NotificationDTO notificationDTO);
        Task DeleteNotificationAsync(long id);
    }

}
