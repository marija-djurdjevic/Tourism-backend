using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain.Users;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AccountService : IAccountService
    {
        private ICrudRepository<User> userRepository;
        private ICrudRepository<Person> personRepository;

        public AccountService(ICrudRepository<User> userRepository, ICrudRepository<Person> personRepository)
        {
            this.userRepository = userRepository;
            this.personRepository = personRepository;
        }
        public Result<PagedResult<AccountReviewDto>> GetPaged(int page, int pageSize)
        {
            var persons = personRepository.GetPaged(page, pageSize);
            var dtos = new List<AccountReviewDto>();
            foreach (var person in persons.Results)
            {
                var user = userRepository.Get(person.UserId);
                var accountDto = new AccountReviewDto();
                accountDto.Id = user.Id;
                accountDto.FirstName = person.Name;
                accountDto.LastName = person.Surname;
                accountDto.Username = user.Username;
                accountDto.Role = user.Role.ToString();
                accountDto.IsActive = user.IsActive;
                accountDto.Email = person.Email;
                dtos.Add(accountDto);
            }
            PagedResult<AccountReviewDto> pageResult = new PagedResult<AccountReviewDto>(dtos, dtos.Count);
            Result<PagedResult<AccountReviewDto>> result = new Result<PagedResult<AccountReviewDto>>();
            result.WithValue(pageResult);
            return result;
        }

        public Result<AccountReviewDto> GetAccount(int id)
        {
            var person = personRepository.Get((long)id);
            var user = userRepository.Get(person.UserId);
            var accountDto = new AccountReviewDto();
            accountDto.Id = user.Id;
            accountDto.Username = user.Username;
            accountDto.Role = user.Role.ToString();
            accountDto.IsActive = user.IsActive;
            accountDto.Email = person.Email;
            Result<AccountReviewDto> result = new Result<AccountReviewDto>();
            result.WithValue(accountDto);
            return result;
        }

        public Result<AccountReviewDto> BlockAccount(AccountReviewDto accountReview)
        {
            var user = userRepository.Get(accountReview.Id);
            user.IsActive = false;
            accountReview.IsActive = false;
            userRepository.Update(user);

            Result<AccountReviewDto> result = new Result<AccountReviewDto>();
            result.WithValue(accountReview);
            return result;
        }
    }
}
