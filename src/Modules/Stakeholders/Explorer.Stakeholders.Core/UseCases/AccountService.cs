using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AccountService : CrudService<AccountReviewDto, User>, IAccountService
    {
        public AccountService(ICrudRepository<User> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
