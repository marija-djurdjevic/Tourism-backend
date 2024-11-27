using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.WalletDtos;
using Explorer.Payments.API.Internal.Wallet;
using Explorer.Payments.API.Public.Wallet;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Wallets;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Wallet
{
    public class WalletService : CrudService<WalletDto, Domain.Wallets.Wallet>, IWalletInternalService, IWalletService
    {
        private readonly IWalletRepository walletRepository;

        public WalletService(IWalletRepository walletRepository, IMapper mapper)
            : base(walletRepository, mapper)
        {
            this.walletRepository = walletRepository;
        }

        public Result<WalletDto> GetByTouristId(int touristId)
        {
            var wallet = walletRepository.GetByTouristId(touristId);
            if (wallet == null)
            {
                return Result.Fail<WalletDto>("Wallet not found for the specified tourist ID.");
            }

            return MapToDto(wallet);
        }
    }
}
