using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.EncounterExecutions;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Domain.Secrets;
using Explorer.Encounters.Core.Mappers;
using Explorer.Encounters.Core.UseCases;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Encounters.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure
{
    public static class EncountersStartup
    {
        public static IServiceCollection ConfigureEncountersModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EncountersProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IEncounterService, EncounterService>();
            services.AddScoped<IEncounterRepository, EncounterRepository>();
            services.AddScoped<IEncounterExecutionService, EncounterExecutionService>();
            services.AddScoped<IEncounterExecutionRepository, EncounterExecutionRepository>();

            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<IStoryUnlockedService, StoryUnlockedService>();
            services.AddScoped<IStoryUnlockedRepository, StoryUnlockedRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<Encounter>), typeof(CrudDatabaseRepository<Encounter, EncountersContext>));
            services.AddScoped(typeof(ICrudRepository<EncounterExecution>), typeof(CrudDatabaseRepository<EncounterExecution, EncountersContext>));
            services.AddScoped(typeof(ICrudRepository<Story>), typeof(CrudDatabaseRepository<Story, EncountersContext>));
            services.AddScoped(typeof(ICrudRepository<StoryUnlocked>), typeof(CrudDatabaseRepository<StoryUnlocked, EncountersContext>));
            services.AddScoped(typeof(ICrudRepository<Book>), typeof(CrudDatabaseRepository<Book, EncountersContext>));

            services.AddDbContext<EncountersContext>(opt =>
             opt.UseNpgsql(DbConnectionStringBuilder.Build("encounters"),
               x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounters")));
        }


    }
}
