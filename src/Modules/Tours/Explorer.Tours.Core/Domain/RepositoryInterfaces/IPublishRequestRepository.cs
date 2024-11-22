using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain.TourProblems;
using Explorer.Tours.Core.Domain.PublishRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IPublishRequestRepository
    {
        public PagedResult<PublishRequest> GetPaged(int page, int pageSize);

        public PublishRequest Create(PublishRequest entity);

    }
}
