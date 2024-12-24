using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class CommentRepository : CrudDatabaseRepository<Comment, BlogContext>, ICommentRepository
    {
        public CommentRepository(BlogContext dbContext) : base(dbContext) { }

        public List<Comment> GetByBlogId(int blogId)
        {
            return DbContext.Comment
                .Where(c => c.BlogId.Equals(blogId))
                .OrderBy(c => c.CreationDate)
                .ToList();
        }

        public List<Comment> GetLatestComments()
        {
            DateTime lastWeek = DateTime.UtcNow.AddDays(-7);

            return DbContext.Comment
                .Where(comment => comment.CreationDate >= lastWeek)
                .ToList();
        }
    }
}
