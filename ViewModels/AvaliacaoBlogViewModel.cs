using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class AvaliacaoBlogViewModel
    {
        public int BlogId { get; set; }
        public string NomeBlog { get; set; }

        [Range(0, 5)]
        [Required(ErrorMessage ="A nota é obrigatória!")]
        public int? Nota { get; set; }
    }
}
