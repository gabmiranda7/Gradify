using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gradify.Models
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Materia { get; set; } = string.Empty;

        [Required]
        public string DiaDaAula { get; set; } = string.Empty;

        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }

        public Professor Professor { get; set; }

        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
        public ICollection<Anotacao> Anotacoes { get; set; } = new List<Anotacao>();
        public ICollection<Frequencia> Frequencias { get; set; } = new List<Frequencia>();
    }
}