using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.ShoppingDtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Shopping
{
    public interface IShoppingService
    {
        public Result<ShoppingCartDto> Checkout(List<OrderItemDto> items, int touristId);
        public Result<List<int>> GetPurchasedToursIds(int touristId);
    }
}
