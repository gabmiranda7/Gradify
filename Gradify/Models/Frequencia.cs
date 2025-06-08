namespace Gradify.Models
{
    public class Frequencia
    {
        public int Id { get; set; }
        public DateTime DataFrequencia { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; } = null!;

        public int TurmaId { get; set; }
        public Turma Turma { get; set; } = null!;
    }
}