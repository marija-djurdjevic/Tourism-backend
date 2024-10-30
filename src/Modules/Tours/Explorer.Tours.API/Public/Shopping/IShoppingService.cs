using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifeCycleDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Shopping
{
    public interface IShoppingService
    {
        public Result<List<TourDto>> GetAllPublished(int page, int pageSize);
    }
}
