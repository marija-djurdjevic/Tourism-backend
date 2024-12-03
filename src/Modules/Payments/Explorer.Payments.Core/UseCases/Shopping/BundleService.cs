using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Dtos.WalletDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Shopping;
using Explorer.Payments.Core.Domain.Wallets;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class BundleService : CrudService<BundleDto, Bundle>, IBundleService
    {
        private readonly IBundleRepository bundleRepository;
        public BundleService(IBundleRepository bundleRepository, IMapper mapper) : base(bundleRepository, mapper)
        {
            this.bundleRepository = bundleRepository;
        }

        public Result<List<BundleDto>> GetByAuthorId(int auhtorId)
        {
            List<Bundle>? bundles = bundleRepository.GetByAuthorId(auhtorId);
            if (bundles == null)
            {
                return Result.Fail<List<BundleDto>>("No bundle found for the specified author ID.");
            }
            return MapToDto(bundles);
        }

        public Result<BundleDto> GetById(int bundleId)
        {
            Bundle? bundle = bundleRepository.Get(bundleId);
            if (bundle == null)
            {
                return Result.Fail<BundleDto>("No bundle found for the specified ID.");
            }
            return MapToDto(bundle);
        }
    }
}
