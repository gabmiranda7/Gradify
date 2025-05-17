namespace Gradify.Models
{
    public class Professor : Usuario
    {
        public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
        public ICollection<Turma> Turmas { get; set; } = new List<Turma>();
    }
}