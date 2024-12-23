using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos.GroupTourDtos;
using Explorer.Tours.API.Public.Authoring;
using Explorer.Tours.Core.Domain.GroupTours;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

public class GroupTourExecutionService : CrudService<GroupTourExecutionDto, GroupTourExecution>, IGroupTourExecutionService
{
    private readonly IMapper _mapper;
    private readonly IGroupTourExecutionRepository _groupTourExecutionRepository;

    public GroupTourExecutionService(ICrudRepository<GroupTourExecution> repository, IMapper mapper, IGroupTourExecutionRepository groupTourExecutionRepository)
        : base(repository, mapper)
    {
        _groupTourExecutionRepository = groupTourExecutionRepository;
        _mapper = mapper;
    }

    public async Task<Result> CancelParticipation(int touristId, int groupTourId)
    {
        var groupTourExecution = await _groupTourExecutionRepository.GetByIdAsync(touristId, groupTourId);
        if (groupTourExecution == null)
        {
            return Result.Fail("GroupTourExecution not found");
        }

        try
        {
            CrudRepository.Delete(groupTourExecution.Id);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"An error occurred while deleting the GroupTourExecution: {ex.Message}");
        }
    }
}
