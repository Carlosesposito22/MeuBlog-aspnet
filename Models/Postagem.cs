using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    [Table("POSTAGENS")]
    public class Postagem
    {
        [Key]
        public int PostagemId { get; set; }

        [Required]
        [StringLength(60)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(3000)]
        public string Conteudo { get; set; }

        [StringLength(50)]
        public string Descricao { get; set; }

        public DateTime DataPublicacao { get; set; }
        public string ImagemUrl { get; set; }

        public string UserId { get; set; }
        public virtual User Autor { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public virtual List<Comentario> Comentarios { get; set; }
    }
}
