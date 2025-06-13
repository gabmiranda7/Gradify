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
        public int AulaId { get; set; }
        public Aula Aula { get; set; }
        public bool Presente { get; set; }  // <-- Adicione esta linha
        public DateTime DataRegistro { get; set; }


    }
}