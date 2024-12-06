
using ExpenseTrackerAPI.Models.DTO;
using ExpenseTrackerAPI.Models.Entities;
using ExpenseTrackerAPI.services.Interfaces;

using Microsoft.AspNetCore.Mvc;


namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications = await _notificationService.GetNotificationsAsync();
            return Ok(notifications);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(long id)
        {
            var notification = await _notificationService.GetNotificationAsync(id);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(NotificationDTO notificationDTO)
        {
            var createdNotification = await _notificationService.CreateNotificationAsync(notificationDTO);
            return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.NotificationID }, createdNotification);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(long id, NotificationDTO notificationDTO)
        {
            try
            {
                await _notificationService.UpdateNotificationAsync(id, notificationDTO);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(long id)
        {
            await _notificationService.DeleteNotificationAsync(id);
            return NoContent();
        }
    }

}
