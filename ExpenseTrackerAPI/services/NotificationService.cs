using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Mappers;
using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;
using ExpenseTrackerAPI.services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificationDTO>> GetNotificationsAsync()
        {
            var notifications = await _context.Notifications.ToListAsync();
            return notifications.Select(NotificationMapper.ToDTO);
        }

        public async Task<NotificationDTO> GetNotificationAsync(long id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            return notification == null ? null : NotificationMapper.ToDTO(notification);
        }

        public async Task<NotificationDTO> CreateNotificationAsync(NotificationDTO notificationDTO)
        {
            var notification = NotificationMapper.ToEntity(notificationDTO);
            notification.CreatedAt = DateTime.UtcNow;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return NotificationMapper.ToDTO(notification);
        }

        public async Task UpdateNotificationAsync(long id, NotificationDTO notificationDTO)
        {
            if (id != notificationDTO.NotificationID)
                throw new ArgumentException("Notification ID mismatch");

            var notification = NotificationMapper.ToEntity(notificationDTO);
            _context.Entry(notification).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(long id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }

}
