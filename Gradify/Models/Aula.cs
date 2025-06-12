using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gradify.Models
{
    public class Aula
    {
        public int Id { get; set; }
        [Required]
        public DateTime DataAula { get; set; }
        [Required]
        public string Tema { get; set; } = string.Empty;
        [Required]
        public int TurmaId { get; set; }
        [ForeignKey("TurmaId")]
        public Turma? Turma { get; set; }
        public ICollection<Anotacao> Anotacoes { get; set; }

    }
}