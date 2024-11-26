using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingDtos;
using Explorer.Payments.API.Public.Shopping;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Domain.Shopping;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases.Shopping
{
    public class SaleService : CrudService<SaleDto, Sale>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        public SaleService(ISaleRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _saleRepository = repository;
        }

        public Result<SaleDto> GetSaleById(int id)
        {
            return MapToDto(_saleRepository.GetSaleById(id));
        }
    }
}
