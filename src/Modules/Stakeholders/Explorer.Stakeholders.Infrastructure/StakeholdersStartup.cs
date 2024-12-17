using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Application.Services;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.Users;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IUserRatingService, UserRatingService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAchievementService, AchievementService>();
        services.AddScoped<IAchievementInternalService, AchievementService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<UserProfile>), typeof(CrudDatabaseRepository<UserProfile, StakeholdersContext>));
        services.AddScoped<IUserRatingRepository, UserRatingDatabaseRepository>();
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped<IAchievementRepository, AchievementRepository>();

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}