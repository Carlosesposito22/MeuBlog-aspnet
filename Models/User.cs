using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class User : IdentityUser
    {
        public virtual List<InscricaoBlog> Inscricoes { get; set; }
        public virtual List<Postagem> Postagens { get; set; }
        public virtual List<Comentario> Comentarios { get; set; }
    }
}
