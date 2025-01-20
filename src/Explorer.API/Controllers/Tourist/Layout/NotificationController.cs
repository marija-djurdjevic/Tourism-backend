using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.UseCases.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Layout
{
    [Route("api/tourist/notification")]
    [Authorize(Policy = "touristPolicy")]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("getUnread")]
        public ActionResult GetUnreadNotifications([FromQuery] int touristId)
        {
            var result = _notificationService.GetUnreadNotificationsByReciever(touristId);
            return CreateResponse(result);
        }

        [HttpPut("setSeen")]
        public ActionResult<TourPreferencesDto> Update([FromBody] NotificationDto notification)
        {
            notification.IsRead = true;
            var result = _notificationService.Update(notification);
            return CreateResponse(result);
        }
        
        [HttpPut("delete")]
        public ActionResult<TourPreferencesDto> Delete([FromBody] NotificationDto notification)
        {
            notification.IsDeleted = true;
            var result = _notificationService.Update(notification);
            return CreateResponse(result);
        }
    }
}
