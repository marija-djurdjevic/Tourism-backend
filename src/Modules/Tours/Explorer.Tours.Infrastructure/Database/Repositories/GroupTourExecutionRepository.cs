using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.GroupTours;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

public class GroupTourExecutionRepository : CrudDatabaseRepository<GroupTourExecution, ToursContext>, IGroupTourExecutionRepository
{
    private readonly ToursContext _dbContext;

    public GroupTourExecutionRepository(ToursContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public GroupTourExecution GetById(int touristId, int groupTourId)
    {
        return _dbContext.GroupTourExecution
                         .AsNoTracking()
                         .FirstOrDefault(k => k.GroupTourId == groupTourId && k.TouristId == touristId);
    }
}