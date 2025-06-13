namespace Gradify.DTOs
{
    public class TurmaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int ProfessorId { get; set; }
        public string NomeProfessor { get; set; } = string.Empty;

        public int CursoId { get; set; }
        public string? NomeCurso { get; set; }

    }
}