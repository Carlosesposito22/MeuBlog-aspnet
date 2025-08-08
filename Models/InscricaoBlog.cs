using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    [Table("INSCRICOES_BLOG")]
    public class InscricaoBlog
    {
        [Key]
        public int InscricaoBlogId { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        [Required]
        public DateTime DataInscricao { get; set; }

        public bool IsAdmin { get; set; }
    }
}
