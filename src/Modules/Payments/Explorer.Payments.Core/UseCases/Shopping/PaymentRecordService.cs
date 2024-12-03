using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Shopping;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class PaymentRecordService : CrudService<PaymentRecordDto, PaymentRecord>, IPaymentRecordService
    {
        private readonly IPaymentRecordRepository _paymentRecordRepository;
        private readonly ITourPurchaseTokenRepository tourPurchaseTokenRepository;
        private readonly IMapper mapper;
        public PaymentRecordService(IPaymentRecordRepository paymentRecordRepository, ITourPurchaseTokenRepository tokenRepository, IMapper mapper) : base(paymentRecordRepository, mapper)
        {
            _paymentRecordRepository = paymentRecordRepository;
            this.tourPurchaseTokenRepository = tokenRepository;
            this.mapper = mapper;
        }


        public Result<PaymentRecordDto> Purchase(BundleDto bundleDto, int touristId)
        {
            var bundle = mapper.Map<BundleDto, Bundle>(bundleDto);
            foreach (int id in bundle.TourIds)
            {
                var token = new TourPurchaseToken(touristId, id, false);
                tourPurchaseTokenRepository.Create(token);
            }

            var payment = new PaymentRecord(touristId, bundle.Id, bundle.Price, DateTime.UtcNow);
            _paymentRecordRepository.Create(payment);

            return MapToDto(payment);
        }
    }
}
