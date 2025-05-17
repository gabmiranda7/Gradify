namespace Gradify.DTOs
{
    public class AulaDto
    {
        public int Id { get; set; }
        public DateTime DataAula { get; set; }
        public TimeSpan HoraInicioChamada { get; set; }
        public TimeSpan HoraFimChamada { get; set; }
        public int TurmaId { get; set; }
    }
}