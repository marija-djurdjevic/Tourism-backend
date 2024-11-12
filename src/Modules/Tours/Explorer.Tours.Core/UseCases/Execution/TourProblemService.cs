using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourProblems;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Execution
{
    public class TourProblemService : BaseService<TourProblemDto, TourProblem>, ITourProblemService
    {
        private readonly IMapper _mapper;
        private readonly ITourProblemRepository _tourProblemRepository;

        public TourProblemService(IMapper mapper, ITourProblemRepository repository) : base(mapper)
        {
            _mapper = mapper;
            _tourProblemRepository = repository;
        }

        public Result<TourProblemDto> Create(TourProblemDto tourProblemDto)
        {
            var tourProblem = _tourProblemRepository.Create(MapToDomain(tourProblemDto));
            return Result.Ok(tourProblemDto);
        }
        public Result<TourProblemDto> Update(TourProblemDto tourProblemDto)
        {
            var tourProblem = _tourProblemRepository.Update(MapToDomain(tourProblemDto));
            return Result.Ok(tourProblemDto);
        }
        public Result<TourProblemDto> AddComment(int problemId, ProblemCommentDto commentDto)
        {
            var tourProblem = _tourProblemRepository.Get(problemId);
            if (tourProblem == null)
                return Result.Fail("Problem not found.");

            tourProblem.AddComment(_mapper.Map<ProblemCommentDto, ProblemComment>(commentDto));

            _tourProblemRepository.Update(tourProblem);

            var resultDto = MapToDto(tourProblem);
            return Result.Ok(resultDto);
        }

        public Result<TourProblemDto> ChangeStatus(int problemId, API.Dtos.TourProblemDtos.ProblemStatus status)
        {
            var tourProblem = _tourProblemRepository.Get(problemId);
            if (tourProblem == null)
                return Result.Fail("Tour problem not found");

            tourProblem.ChangeStatus((Domain.TourProblems.ProblemStatus)status);
            _tourProblemRepository.Update(tourProblem);

            return Result.Ok(MapToDto(tourProblem));
        }

        public Result<PagedResult<TourProblemDto>> GetAll()
        {
            var pagedResult = _tourProblemRepository.GetPaged(0, 0);
            var openTourProblems = pagedResult.Results
                .Where(tp => tp.Status != Domain.TourProblems.ProblemStatus.Closed)
                .ToList();

            var mappedResults = MapToDto(openTourProblems);

            return new PagedResult<TourProblemDto>(
                mappedResults.Value,
                totalCount: mappedResults.Value.Count 
            );
        }

        public Result<TourProblemDto> GetById(int id)
        {
            var tourProblem = _tourProblemRepository.Get(id);
            if (tourProblem == null)
                return Result.Fail("Tour problem not found");

            return Result.Ok(MapToDto(tourProblem));
        }

        public Result<TourProblemDto> SetDeadline(int problemId, DateTime deadline, int receiverId)
        {
            var tourProblem = _tourProblemRepository.Get(problemId);
            if (tourProblem == null)
                return Result.Fail("Tour problem not found");

            tourProblem.SetDeadline(deadline, receiverId); 

            _tourProblemRepository.Update(tourProblem);

            return Result.Ok(MapToDto(tourProblem));
        }

        public Result<PagedResult<TourProblemDto>> GetByToursIds(List<int> ids)
        {
            var results = GetAll().Value.Results.Where(x => ids.Contains(x.TourId)).ToList();

            return new PagedResult<TourProblemDto>(
                results,
                totalCount: results.Count
            );
        }

        public Result<List<TourProblemDto>> GetByTouristId(int id)
        {
            var results = GetAll().Value.Results.Where(x => x.TouristId == id).ToList();
            return results;
        }

        public Result<TourProblemDto> SetProblemClosed(TourProblemDto tourProblemDto)
        {
            tourProblemDto.Status = API.Dtos.TourProblemDtos.ProblemStatus.Closed;
            var tourProblem = _tourProblemRepository.Update(MapToDomain(tourProblemDto));

            if (tourProblem == null)
                return Result.Fail("Tour problem not found");

            return Result.Ok(tourProblemDto);
        }
    }
}
