using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;
using sobujayonApp.Core.RepositoryContracts;
using sobujayonApp.Core.ServiceContracts;

namespace sobujayonApp.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(IRepository<Blog> blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogResponse>> GetBlogs(string? search, int page, int limit)
        {
             var blogs = await _blogRepository.GetAllAsync();
             var query = blogs.AsQueryable();

             if (!string.IsNullOrEmpty(search))
                 query = query.Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

             var paged = query.Skip((page - 1) * limit).Take(limit);
             return _mapper.Map<IEnumerable<BlogResponse>>(paged);
        }

        public async Task<BlogDetailsResponse?> GetBlogBySlug(string slug)
        {
            var blog = await _blogRepository.GetAsync(b => b.Slug == slug);
            return _mapper.Map<BlogDetailsResponse>(blog);
        }

        public async Task<BlogResponse> CreateBlog(CreateBlogRequest request)
        {
            var blog = _mapper.Map<Blog>(request);
            if(string.IsNullOrEmpty(blog.Slug)) blog.Slug = blog.Title.ToLower().Replace(" ", "-");
            await _blogRepository.AddAsync(blog);
            return _mapper.Map<BlogResponse>(blog);
        }

        public async Task<BlogResponse?> UpdateBlog(string id, CreateBlogRequest request)
        {
            int blogId = int.Parse(id);
            var blog = await _blogRepository.GetByIdAsync(blogId);
            if (blog == null) return null;

            _mapper.Map(request, blog);
             await _blogRepository.UpdateAsync(blog);
             return _mapper.Map<BlogResponse>(blog);
        }

        public async Task<bool> DeleteBlog(string id)
        {
            int blogId = int.Parse(id);
             var blog = await _blogRepository.GetByIdAsync(blogId);
             if (blog == null) return false;
             
             await _blogRepository.DeleteAsync(blog);
             return true;
        }
    }
}
