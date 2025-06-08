namespace Gradify.Models
{
    public class Aula
    {
        public int Id { get; set; }
        public DateTime DataAula { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; } = null!;
    }
}