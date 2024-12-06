using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;

namespace ExpenseTrackerAPI.Mappers
{
    public static class NotificationMapper
    {
        public static NotificationDTO ToDTO(Notification notification)
        {
            return new NotificationDTO
            {
                NotificationID = notification.NotificationID,
                UserId = notification.UserId,
                Message = notification.Message,
                Status = notification.Status.ToString(),
                CreatedAt = notification.CreatedAt
            };
        }

        public static Notification ToEntity(NotificationDTO dto)
        {
            return new Notification
            {
                NotificationID = dto.NotificationID,
                UserId = dto.UserId,
                Message = dto.Message,
                Status = Enum.TryParse(dto.Status, out NotificationStatus status) ? status : NotificationStatus.Pending,
                CreatedAt = dto.CreatedAt
            };
        }
    }
}
