using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogRepository : CrudDatabaseRepository<Blogs, BlogContext>, IBlogRepository
    {
        public BlogRepository(BlogContext dbContext) : base(dbContext)
        {
        }

        public Blogs GetBlogById(int id)
        {
            var blog = DbContext.Blogs.FirstOrDefault(c => c.Id == id);

            return blog == null ? throw new Exception("Blog not found.") : blog;
        }
    }
}
