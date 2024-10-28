using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourProblemDtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.TourProblems;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourProblemService : CrudService<TourProblemDto, TourProblem>, ITourProblemService
    {
        public TourProblemService(ICrudRepository<TourProblem> repository, IMapper mapper) : base(repository, mapper) { }

        public override Result<TourProblemDto> Create(TourProblemDto problem)
        {
           // problem.Details =new ProblemDetails (Enum.Parse     (typeof        (Explorer.Tours.Core.Domain.TourProblems.ProblemDetails.ProblemCategory , problem.Details.Category.ToString() )), problem.Details.ProblemPriority, problem.Details.Explanation, DateTime.Now);

          //  problem.Details.Time = new ProblemDetails
            return base.Create(problem);
        }

    }
}
