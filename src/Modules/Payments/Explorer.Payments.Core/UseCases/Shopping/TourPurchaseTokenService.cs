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
        private readonly IMapper _mapper;
        public TourPurchaseTokenService(ITourPurchaseTokenRepository repository, IMapper mapper) : base(repository, mapper) { 
            _repository = repository;
            _mapper = mapper;
        }


        public Result<List<int>> GetPurchasedTours(int touristId)
        {
            // Delegate to the repository to fetch purchased tours
            return _repository.GetPurchasedTours(touristId);
        }

        public Result<int> RefundPurchasedTour(int tourId, int touristId)
        {
            return _repository.RefundPurchasedTour(tourId, touristId);
        }

        public Result<int> getTourByPurchaseId(int purchaseId)
        {
            return _repository.Get(purchaseId).TourId;
        }

        public TourPurchaseTokenDto FindByTourAndTourist(int tourId, int touristId)
        {
            var token = _repository.FindByTourAndTourist(tourId, touristId);
            return _mapper.Map<TourPurchaseTokenDto>(token);
        }
    }
}
