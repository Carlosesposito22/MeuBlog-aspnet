using Blog.Data;
using Blog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;
        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Blog> Blogs => _context.Blogs.Include(b => b.Admin);

        public async Task CreateBlogAsync(Models.Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
        }
    }
}
