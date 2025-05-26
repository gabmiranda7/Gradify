namespace Gradify.DTOs
{
    public class ProfessorDto : UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<CursoDto> Cursos { get; set; } = new List<CursoDto>();
        public ICollection<TurmaDto> Turmas { get; set; } = new List<TurmaDto>();
    }
}