using Blog.Models;
using Blog.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;

        public BlogController(IBlogRepository blogRepository, UserManager<User> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.FindAllAsync();
            return View(blogs);
        }

        [HttpGet]
        public IActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBlog(Models.Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.UserId = _userManager.GetUserId(User);
                blog.Criacao = DateTime.Now;

                await _blogRepository.CreateBlogAsync(blog);

                return RedirectToAction("Index", "Blog");
            }
            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> EditBlog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _blogRepository.FindByIdAsync((int)id);

            if (blog == null)
            {
                return NotFound();
            }

            if (blog.Admin.Id != _userManager.GetUserId(User))
            {
                return NotFound();
            }
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBlog(int id, Models.Blog blog)
        {
            if (ModelState.IsValid)
            {
                if (id != blog.BlogId)
                {
                    return NotFound();
                }

                await _blogRepository.UpdateBlogAsync(id, blog);

                return RedirectToAction("Index", "Blog");
            }
            return View(blog);
        }
    }
}
