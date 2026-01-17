using System.Collections.Generic;
using System.Threading.Tasks;
using sobujayonApp.Core.DTO;

namespace sobujayonApp.Core.ServiceContracts
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogResponse>> GetBlogs(string? search, int page, int limit);
        Task<BlogDetailsResponse?> GetBlogBySlug(string slug);
        Task<BlogResponse> CreateBlog(CreateBlogRequest request);
        Task<BlogResponse?> UpdateBlog(string id, CreateBlogRequest request);
        Task<bool> DeleteBlog(string id);
    }
}
