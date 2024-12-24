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

        public List<Blogs> GetTop3BLogs(List<Comment> latestComments)
        {
            var lastWeek = DateTime.Now.AddDays(-7);

            var commentCounts = latestComments
                .GroupBy(comment => comment.BlogId)
                .Select(group => new
                {
                    BlogId = group.Key,
                    CommentCount = group.Count()
                })
                .ToList();

            var blogsWithInteractionCounts = DbContext.Blogs
                .AsEnumerable() 
                .Select(blog => new
                {
                    Blog = blog,
                    InteractionCount = (commentCounts.FirstOrDefault(cc => cc.BlogId == blog.Id)?.CommentCount ?? 0) +
                                       blog.Votes.Count(vote => vote.CreationDate >= lastWeek)
                })
                .OrderByDescending(blogWithCount => blogWithCount.InteractionCount)
                .Take(3) 
                .Select(blogWithCount => blogWithCount.Blog)
                .ToList();

            return blogsWithInteractionCounts;
        }
    }
}