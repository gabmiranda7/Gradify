namespace Gradify.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public int ProfessorId { get; set; }
        public Professor Professor { get; set; } = null!;

        public ICollection<Anotacao> Anotacoes { get; set; } = new List<Anotacao>();
        public ICollection<Aula> Aulas { get; set; } = new List<Aula>();
        public ICollection<TurmaCurso> TurmasCursos { get; set; } = new List<TurmaCurso>();
    }
}