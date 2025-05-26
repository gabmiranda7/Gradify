using Gradify.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gradify.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public ICollection<AlunoTurma> AlunosTurmas { get; set; } = new List<AlunoTurma>();
        public ICollection<Anotacao> Anotacoes { get; set; } = new List<Anotacao>();
        public ICollection<Aula> Aulas { get; set; } = new List<Aula>();
        public ICollection<Frequencia> Frequencias { get; set; } = new List<Frequencia>();
        public ICollection<TurmaCurso> TurmasCursos { get; set; } = new List<TurmaCurso>();
    }
}