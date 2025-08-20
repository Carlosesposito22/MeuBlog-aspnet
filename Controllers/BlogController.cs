using Blog.Models;
using Blog.Repositories.Interfaces;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogRepository.FindAllAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogRepository.GetBlogDetailsById(id);

            if ((blog == null) || (!IsUserSubscribe(blog.Inscritos)))
            {
                return NotFound();
            }

            float mediaAvaliacao = (blog.Inscritos.Sum(i => i.Avaliacao) / blog.Inscritos.Count);

            var blogDetailsVM = new BlogDetailsViewModel()
            {
                BlogId = blog.BlogId,
                Nome = blog.Nome,
                Descricao = blog.Descricao,
                Criacao = blog.Criacao,
                AdminId = blog.UserId,
                MediaDeAvaliacao = mediaAvaliacao,
                QuantidadeInscritos = blog.Inscritos.Count,
                Postagens = blog.Postagens,
                InscricaoBlogs = blog.Inscritos,
                IsUserAdmin = IsUserAdmin(blog)
            }; 
            return View(blogDetailsVM);
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
        public async Task<IActionResult> EditBlog(int id)
        {
            var blog = await _blogRepository.FindByIdAsync(id);

            if (!IsUserAdmin(blog))
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

        // Delete Blog mais pra frente!

        [HttpGet]
        public async Task<IActionResult> Subscribe(int id)
        {
            var blog = await _blogRepository.FindByIdAsync(id);

            if (blog == null) return NotFound();

            return PartialView("_SubscribeModalPartial", blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubscribePost(int id)
        {
            var blog = await _blogRepository.GetBlogDetailsById(id);

            if (blog == null) return NotFound();

            if (!IsUserSubscribe(blog.Inscritos))
            {
                await _blogRepository.SubscribeUserAsync(id, _userManager.GetUserId(User));
            }
            return RedirectToAction("Details", new { id });
        }

        // === GET e POST para avaliar blog ===

        // Alterar o visual de details para ficar mais amigavel
        // Funcionalidade de Postagem e comentário --> bem longa

        private bool IsUserSubscribe(List<InscricaoBlog> inscricoes)
        {
            return (inscricoes.FirstOrDefault(i => i.UserId == _userManager.GetUserId(User)) != null);
        }

        private bool IsUserAdmin(Models.Blog blog)
        {
            if (blog == null)
            {
                return false;
            }
            if (blog.Admin.Id != _userManager.GetUserId(User))
            {
                return false;
            }
            return true;
        }
    }
}
