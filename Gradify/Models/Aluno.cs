using System.Collections.Generic;

namespace Gradify.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public int? TurmaId { get; set; }
        public Turma? Turma { get; set; } = null!;

        public string UsuarioId { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;

        public ICollection<Anotacao> Anotacoes { get; set; } = new List<Anotacao>();
        public ICollection<Frequencia> Frequencias { get; set; } = new List<Frequencia>();
    }
}