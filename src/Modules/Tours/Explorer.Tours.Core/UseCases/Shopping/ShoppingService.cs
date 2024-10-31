using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.ShoppingDtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using Explorer.Tours.API.Dtos.TourSessionDtos;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.TourSessions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Shopping
{
    public class ShoppingService : CrudService<ShoppingCartDto, ShoppingCart>, IShoppingService
    {
        ICrudRepository<ShoppingCart> _repository;
        ICrudRepository<TourPurchaseToken> _repositoryTourPurchaseTokenRepository;
        private readonly IMapper _mapper;

        public ShoppingService(ICrudRepository<ShoppingCart> repository,ICrudRepository<TourPurchaseToken> rep, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _repositoryTourPurchaseTokenRepository = rep;
            _mapper = mapper;
            
        }

        public Result<ShoppingCartDto> Checkout(List<OrderItemDto> items,int touristId)
        {
            var orderItems = items.Select(dto => _mapper.Map<OrderItemDto, OrderItem>(dto)).ToList();
            var tokens = new List<TourPurchaseToken>();

            foreach (var item in orderItems)
            {
                var token = new TourPurchaseToken(touristId, item.TourId);
                _repositoryTourPurchaseTokenRepository.Create(token);

            }
            ShoppingCart cart = new ShoppingCart(orderItems,tokens);
            cart.CalculatePrice();
            var res = _repository.Create(cart);
            return Result.Ok(_mapper.Map<ShoppingCart, ShoppingCartDto>(cart));
        }
    }
}
