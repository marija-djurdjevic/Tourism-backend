using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Public.Shopping;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class ShoppingService : BaseService<ShoppingCartDto, ShoppingCart>, IShoppingService
    {
        ICrudRepository<ShoppingCart> _shoppingRepository;
        ITourPurchaseTokenRepository _tourPurchaseTokenRepository;
        private readonly IMapper _mapper;

        public ShoppingService(ICrudRepository<ShoppingCart> shoppingRepository, ITourPurchaseTokenRepository tokenRepository, IMapper mapper) : base(mapper)
        {
            _shoppingRepository = shoppingRepository;
            _tourPurchaseTokenRepository = tokenRepository;
            _mapper = mapper;
            
        }

        public Result<ShoppingCartDto> Checkout(List<OrderItemDto> items,int touristId)
        {
            var orderItems = items.Select(dto => _mapper.Map<OrderItemDto, OrderItem>(dto)).ToList();
            var tokens = new List<TourPurchaseToken>();

            var purchasedToursIds = _tourPurchaseTokenRepository.GetPurchasedTours(touristId);

            var duplicateTours = orderItems
            .Where(item => purchasedToursIds.Contains(item.TourId))
            .Select(item => item.TourId)
            .ToList();

            if (duplicateTours.Any())
            {
                return Result.Fail<ShoppingCartDto>($"The following tours have already been purchased: {string.Join(", ", duplicateTours)}.");
            }
            foreach (var item in orderItems)
            {
                var token = new TourPurchaseToken(touristId, item.TourId);
                _tourPurchaseTokenRepository.Create(token);

            }

            ShoppingCart cart = new ShoppingCart(touristId, orderItems, tokens);
            cart.CalculatePrice();
            var res = _shoppingRepository.Create(cart);
            return Result.Ok(_mapper.Map<ShoppingCart, ShoppingCartDto>(cart));
        }

        public Result<List<int>> GetPurchasedToursIds(int touristId)
        {
            return  _tourPurchaseTokenRepository.GetPurchasedTours(touristId);
        }

    }
}
