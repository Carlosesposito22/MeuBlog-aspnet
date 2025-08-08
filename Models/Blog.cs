using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    [Table("BLOGS")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        public DateTime Criacao { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<Postagem> Postagens { get; set; }

        public virtual List<InscricaoBlog> Inscritos { get; set; }
    }
}
