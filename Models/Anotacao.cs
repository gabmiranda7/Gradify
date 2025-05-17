namespace Gradify.Models
{
    public class Anotacao
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }
    }
}