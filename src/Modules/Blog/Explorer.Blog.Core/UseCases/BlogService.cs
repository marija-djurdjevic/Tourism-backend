using Explorer.Blog.Core.Domain;
using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Blog.API.Public;
using FluentResults;
using AutoMapper;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogService : CrudService<BlogDto, Blogs>, IBlogService
    {

        public BlogService(ICrudRepository<Blogs> repository, IMapper mapper) : base(repository, mapper){}

        public  Result<BlogDto> Get(int id)
        {
            return base.Get(id);
        }

        public override Result<BlogDto> Create(BlogDto blog)
        {
            return base.Create(blog);
        }

        public override Result<BlogDto> Update(BlogDto blog)
        {
            return base.Update(blog);
        }

        public override Result Delete(int id)
        {
            return base.Delete(id);
        }

        public  Result<PagedResult<BlogDto>> GetPaged(int page, int pageSize)
        {
            return base.GetPaged(page, pageSize);
        }
    }
}
