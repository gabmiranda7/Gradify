using Gradify.DTOs;

namespace Gradify.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;

        public ICollection<Anotacao> Anotacoes { get; set; } = new List<Anotacao>();
        public ICollection<AlunoTurma> AlunosTurmas { get; set; } = new List<AlunoTurma>();
        public ICollection<Frequencia> Frequencias { get; set; } = new List<Frequencia>();
        public ICollection<Turma> Turmas { get; set; } = new List<Turma>();
    }
}