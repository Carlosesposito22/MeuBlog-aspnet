using Blog.Data;
using Blog.Models;
using Blog.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Blog>> FindAllAsync()
        {
            return await _context.Blogs
                .Include(b => b.Admin)
                .ToListAsync();
        }

        public async Task<Models.Blog> FindByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.Admin)
                .FirstOrDefaultAsync(b => b.BlogId == id);
        }

        public async Task<Models.Blog> GetBlogDetailsById(int id)
        {
            return await _context.Blogs
                .Include(b => b.Postagens)
                .Include(b => b.Inscritos)
                .FirstOrDefaultAsync(b => b.BlogId == id);
        }

        public async Task CreateBlogAsync(Models.Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            await SubscribeUserAsync(blog.BlogId, blog.UserId);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBlogAsync(int id, Models.Blog newBlog)
        {
            var blog = await FindByIdAsync(id);

            if (blog != null)
            {
                blog.Nome = newBlog.Nome;
                blog.Descricao = newBlog.Descricao;

                _context.Update(blog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SubscribeUserAsync(int blogId, string userId)
        {
            var subscribe = new InscricaoBlog()
            {
                UserId = userId,
                BlogId = blogId,
                DataInscricao = DateTime.Now,
                Avaliacao = 0
            };
            _context.InscricoesBlog.Add(subscribe);
            await _context.SaveChangesAsync();
        }
    }
}
