namespace Gradify.Models
{
    public class Aula
    {
        public int Id { get; set; }
        public DateTime DataAula { get; set; }
        public TimeSpan HoraInicioChamada { get; set; }
        public TimeSpan HoraFimChamada { get; set; }
        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}