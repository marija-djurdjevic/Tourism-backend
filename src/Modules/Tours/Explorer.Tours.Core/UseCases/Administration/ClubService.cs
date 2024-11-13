using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class ClubService : CrudService<ClubDto, Club>, IClubService
    {
        private readonly ICrudRepository<Club> _repository;
        private readonly IMapper _mapper;
        public ClubService(ICrudRepository<Club> repository, IMapper mapper) : base(repository, mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Result<List<ClubDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ClubDto GetById(int id)
        {
            // Retrieve the club entity from the repository using the ID
            var club = _repository.Get(id);

            // If the club doesn't exist, you can return null or throw an exception based on your application's needs
            if (club == null)
            {
                return null; // Or throw an exception if preferred
            }

            // Map the club entity to a ClubDto and return it
            return _mapper.Map<ClubDto>(club);
        }
    }
}
