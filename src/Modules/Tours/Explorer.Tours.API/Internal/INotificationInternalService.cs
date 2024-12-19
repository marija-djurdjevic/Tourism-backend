using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Internal
{
    public interface INotificationInternalService
    {
        Result<PagedResult<NotificationDto>> GetPaged(int page, int pageSize);
        Result<NotificationDto> Create(NotificationDto notification);
        Result<NotificationDto> Update(NotificationDto notification);
        Result Delete(int id);
        Result<List<NotificationDto>> GetUnreadNotificationsByReciever(int userId);
        Task NotifyUserAsync(int userId, NotificationDto message);
    }
}
