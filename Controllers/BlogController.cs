using Blog.Models;
using Blog.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            var blogs = _blogRepository.Blogs;
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
    }
}
