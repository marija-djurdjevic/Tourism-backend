using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class ShoppingService : CrudService<TourDto, Tour>, IShoppingService
    {
        ICrudRepository<Tour> _repository;

        public ShoppingService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
        }

        public Result<List<TourDto>> GetAllPublished(int page, int pageSize)
        {
            var result = _repository.GetPaged(page, pageSize).Results.Where(tour => tour.Status == TourStatus.Published).ToList();
            var dto = MapToDto(result);
            return dto;


        }
    }
}
