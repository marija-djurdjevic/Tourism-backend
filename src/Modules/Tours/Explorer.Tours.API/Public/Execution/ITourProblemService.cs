using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Execution
{
    public interface ITourProblemService
    {
        public Result<TourProblemDto> GetById(int id);
        public Result<PagedResult<TourProblemDto>> GetAll();
        public Result<TourProblemDto> AddComment(int problemId, ProblemCommentDto comment);
        public Result<TourProblemDto> SetDeadline(int problemId, DateTime deadline);
        public Result<TourProblemDto> ChangeStatus(int problemId, ProblemStatus status);
        public Result<List<NotificationDto>> GetUnreadNotifications(int problemId);
    }
}
