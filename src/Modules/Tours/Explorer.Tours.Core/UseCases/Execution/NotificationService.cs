using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourProblems;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class NotificationService : CrudService<NotificationDto, Notification>, INotificationService, INotificationInternalService
    {
        public NotificationService(ICrudRepository<Notification> crudRepository, IMapper mapper) : base(crudRepository, mapper) {}

        public Result<List<NotificationDto>> GetUnreadNotificationsByReciever(int userId)
        {
            var list = GetPaged(0, 0);
            return Result.Ok(list.Value.Results.Where(x => x.RecieverId == userId && !x.IsDeleted /*&& x.IsRead == false*/).ToList());
        }

        public async Task NotifyUserAsync(int userId, NotificationDto notification)
        {
            await WebSocketHandler.SendMessageToUserAsync(userId, notification);
        }
    }
}
