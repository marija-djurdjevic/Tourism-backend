using AutoMapper;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Mappers
{
    public class PaymentsProfile : Profile
    {
        public PaymentsProfile() {

            CreateMap<ShoppingCartDto, ShoppingCart>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)).ReverseMap();
            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<TourPurchaseTokenDto, TourPurchaseToken>().ReverseMap();

        }
    }
}
