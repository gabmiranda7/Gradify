using Gradify.DTOs;

namespace Gradify.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; }

        public ICollection<TurmaCurso> TurmasCursos { get; set; } = new List<TurmaCurso>();
    }
}