using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gradify.Models
{
    public class Frequencia
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }

        [ForeignKey("Aluno")]
        public int AlunoId { get; set; }

        public Aluno Aluno { get; set; } 

        [ForeignKey("Turma")]
        public int TurmaId { get; set; }

        public Turma Turma { get; set; }
    }
}