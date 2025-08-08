using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    [Table("COMENTARIOS")]
    public class Comentario
    {
        [Key]
        public int ComentarioId { get; set; }

        [Required]
        [StringLength(500)]
        public string Conteudo { get; set; }
        public DateTime DataComentario { get; set; }

        public string UserId { get; set; }
        public virtual User Autor { get; set; }

        public int PostagemId { get; set; }
        public virtual Postagem Postagem { get; set; }
    }
}
