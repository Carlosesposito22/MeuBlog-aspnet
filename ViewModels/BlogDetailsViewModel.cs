using Blog.Models;

namespace Blog.ViewModels
{
    public class BlogDetailsViewModel
    {
        public int BlogId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Criacao { get; set; }
        public string AdminId { get; set; }
        public float MediaDeAvaliacao { get; set; }
        public int QuantidadeInscritos { get; set; }

        public IEnumerable<Postagem> Postagens { get; set; }
        public IEnumerable<InscricaoBlog> InscricaoBlogs { get; set; }
    }
}
