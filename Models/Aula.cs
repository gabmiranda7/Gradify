namespace Gradify.Models
{
    public class Aula
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataAula { get; set; }
        public TimeSpan HoraInicioAula { get; set; }
        public TimeSpan HoraFimAula { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}