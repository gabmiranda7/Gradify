namespace Gradify.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; } = null!;

        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
        public ICollection<TurmaCurso> TurmasCursos { get; set; } = new List<TurmaCurso>();
    }
}