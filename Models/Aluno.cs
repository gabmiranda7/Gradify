namespace Gradify.Models
{
    public class Aluno : Usuario
    {
        public string Matricula { get; set; } = string.Empty;

        public ICollection<Anotacao> Anotacoes { get; set; } = new List<Anotacao>();
        public ICollection<AlunoTurma> AlunosTurmas { get; set; } = new List<AlunoTurma>();
        public ICollection<Frequencia> Frequencias { get; set; } = new List<Frequencia>();
    }
}