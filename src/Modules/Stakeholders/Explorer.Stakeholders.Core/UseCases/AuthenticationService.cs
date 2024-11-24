using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal.Wallet;
using Explorer.Payments.API.Dtos.WalletDtos;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using FluentResults;
using UserRole = Explorer.Stakeholders.Core.Domain.Users.UserRole;

namespace Explorer.Stakeholders.Core.UseCases;

public class AuthenticationService : IAuthenticationService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ICrudRepository<Person> _personRepository;
    private readonly ICrudRepository<UserProfile> _userProfileRepository;
    private readonly IWalletInternalService _walletService;


    public AuthenticationService(ICrudRepository<UserProfile> userProfileRepository,IUserRepository userRepository, ICrudRepository<Person> personRepository, ITokenGenerator tokenGenerator, IWalletInternalService walletService)
    {
        _tokenGenerator = tokenGenerator;
        _userRepository = userRepository;
        _personRepository = personRepository;
        _userProfileRepository = userProfileRepository;
        _walletService = walletService;
    }

    public Result<AuthenticationTokensDto> Login(CredentialsDto credentials)
    {
        var user = _userRepository.GetActiveByName(credentials.Username);
        if (user == null || credentials.Password != user.Password) return Result.Fail(FailureCode.NotFound);

        long personId;
        try
        {
            personId = _userRepository.GetPersonId(user.Id);
        }
        catch (KeyNotFoundException)
        {
            personId = 0;
        }
        return _tokenGenerator.GenerateAccessToken(user, personId);
    }

    public Result<AuthenticationTokensDto> RegisterTourist(AccountRegistrationDto account)
    {
        if(_userRepository.Exists(account.Username)) return Result.Fail(FailureCode.NonUniqueUsername);

        try
        {
            var user = _userRepository.Create(new User(account.Username, account.Password, UserRole.Tourist, true));
            var person = _personRepository.Create(new Person(user.Id, account.Name, account.Surname, account.Email));
            var profile = _userProfileRepository.Create(new UserProfile(user.Id, account.Name, account.Surname, null, null, null));
            var wallet = _walletService.Create(new WalletDto(Convert.ToInt32(user.Id)));

            return _tokenGenerator.GenerateAccessToken(user, person.Id);
        }
        catch (ArgumentException e)
        {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            // There is a subtle issue here. Can you find it?
        }
    }
}