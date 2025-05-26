using Gradify.Enums;

namespace Gradify.DTOs
{
    public class UsuarioDto
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public TipoUsuario Tipo { get; set; }
    }
}
