namespace Gradify.DTOs
{
    public class ProfessorDTO : UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public List<string> Cursos { get; set; } = new();
    }
}