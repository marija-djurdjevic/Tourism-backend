using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourProblems;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.Execution;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<ITourEquipmentService, TourEquipmentService>();
        services.AddScoped<ITourReviewService, TourReviewService>();
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<IKeyPointService, KeyPointService>();
        services.AddScoped<IObjectService, ObjectService>();
        services.AddScoped<ITourPreferencesService, TourPreferencesService>();

        services.AddScoped<ITourProblemRepository, TourProblemRepository>();
        services.AddScoped<ITourProblemService, TourProblemService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<TourPreferences>), typeof(CrudDatabaseRepository<TourPreferences, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourEquipment>), typeof(CrudDatabaseRepository<TourEquipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourReview>), typeof(CrudDatabaseRepository<TourReview, ToursContext>));
      
        services.AddScoped(typeof(ICrudRepository<Explorer.Tours.Core.Domain.Object>), typeof(CrudDatabaseRepository<Explorer.Tours.Core.Domain.Object, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<KeyPoint>), typeof(CrudDatabaseRepository<KeyPoint, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourProblem>), typeof(CrudDatabaseRepository<TourProblem, ToursContext>));

        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));

        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("clubs"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "clubs")));
    }
}