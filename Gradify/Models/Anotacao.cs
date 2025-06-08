namespace Gradify.Models
{
    public class Anotacao
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; } = null!;

        public int CursoId { get; set; }
        public Curso Curso { get; set; } = null!;
    }
}