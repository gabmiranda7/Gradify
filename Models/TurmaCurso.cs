namespace Gradify.Models
{
    public class TurmaCurso
    {
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}