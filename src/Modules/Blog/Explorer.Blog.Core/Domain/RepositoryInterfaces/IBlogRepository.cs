using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogRepository : ICrudRepository<Blogs>
    {
        Blogs GetBlogById(int id);
        List<Blogs> GetTop3BLogs(List<Comment> comments);
    }
}