using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Internal.Shopping;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class TourPurchaseTokenService: CrudService<TourPurchaseTokenDto, TourPurchaseToken>, ITourPurchaseTokenService
    {
        private readonly ITourPurchaseTokenRepository _repository;
        public TourPurchaseTokenService(ITourPurchaseTokenRepository repository, IMapper mapper) : base(repository, mapper) { 
            _repository = repository;
        }


        public Result<List<int>> GetPurchasedTours(int touristId)
        {
            // Delegate to the repository to fetch purchased tours
            return _repository.GetPurchasedTours(touristId);
        }

    }
}
