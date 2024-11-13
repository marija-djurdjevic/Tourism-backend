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
            try
            {
                var club = _repository.Get(id);
                return club == null ? null : _mapper.Map<ClubDto>(club);
            }
            catch (KeyNotFoundException)
            {
                return null; // or handle appropriately
            }
        }
    }
}
