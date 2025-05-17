namespace Gradify.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public ICollection<AlunoTurma> AlunosTurmas { get; set; }
        public ICollection<Anotacao> Anotacoes { get; set; }
        public ICollection<Aula> Aulas { get; set; }
        public ICollection<Frequencia> Frequencias { get; set; }
        public ICollection<TurmaCurso> TurmasCursos { get; set; }
    }
}