namespace Gradify.DTOs
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int ProfessorId { get; set; }

        public ICollection<TurmaCursoDto> TurmasCursos { get; set; } = new List<TurmaCursoDto>();
    }
}