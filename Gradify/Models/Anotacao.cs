using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gradify.Models
{
    public class Anotacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Comentario { get; set; } = string.Empty;

        [ForeignKey("Turma")]
        public int TurmaId { get; set; }

        public Turma Turma { get; set; }

        public DateTime DataCriacao { get; set; }  
    }
}