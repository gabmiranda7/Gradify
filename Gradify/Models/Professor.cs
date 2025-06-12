using System.Collections.Generic;

namespace Gradify.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string UsuarioId { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;

    }
}