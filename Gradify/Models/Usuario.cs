using Gradify.Enums;
using Microsoft.AspNetCore.Identity;

namespace Gradify.Models
{
    public class Usuario : IdentityUser
    {
        public TipoUsuario Tipo { get; set; }

        public virtual Aluno? Aluno { get; set; }
        public virtual Professor? Professor { get; set; }
    }
}