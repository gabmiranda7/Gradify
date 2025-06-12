namespace Gradify.DTOs
{
    public class AulaDTO
    {
        public int Id { get; set; }
        public string Tema { get; set; } = string.Empty; // <-- Adicione isso
        public DateTime DataAula { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public int CursoId { get; set; }
        public int TurmaId { get; set; } // Adicione este campo
        public string? NomeTurma { get; set; } // Opcional, se for exibir o nome da turma
    }
}