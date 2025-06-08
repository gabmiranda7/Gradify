namespace Gradify.Models
{
    public class TurmaCurso
    {
        public int CursoId { get; set; }
        public Curso Curso { get; set; } = null!;

        public int TurmaId { get; set; }
        public Turma Turma { get; set; } = null!;
    }
}