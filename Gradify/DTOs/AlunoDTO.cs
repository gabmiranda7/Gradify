using Microsoft.AspNetCore.Identity;

namespace Gradify.DTOs
{
    public class AlunoDTO : UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public int TurmaId { get; set; }
        public string UsuarioId { get; set; } = string.Empty;

        public List<FrequenciaDTO> Frequencias { get; set; } = new();
    }
}