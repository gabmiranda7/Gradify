namespace Gradify.Models
{
    public class Frequencia
    {
        public int Id { get; set; }
        public bool Presente { get; set; }
        public DateTime DataRegistro { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}