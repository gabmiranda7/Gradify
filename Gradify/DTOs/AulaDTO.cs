namespace Gradify.DTOs
{
    public class AulaDTO
    {
        public int Id { get; set; }
        public DateTime DataAula { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public int CursoId { get; set; }
    }
}