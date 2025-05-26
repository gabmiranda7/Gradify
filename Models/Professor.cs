namespace Gradify.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
        public ICollection<Turma> Turmas { get; set; } = new List<Turma>();
    }
}