using Gradify.Models;
using System.ComponentModel.DataAnnotations;

namespace Gradify.DTOs
{
    public class AnotacaoDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo de texto é obrigatório.")]
        public string Texto { get; set; } = string.Empty;
        public int AulaId { get; set; }
        public Aula Aula { get; set; }   // 👈 RELACIONAMENTO
        public string Tema { get; set; } = string.Empty; // Só leitura da aula, não será salvo
        public DateTime DataAula { get; set; }           // Só leitura da aula, não será salvo
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }
        public int? AlunoId { get; set; }
        public string? AlunoNome { get; set; }
        public int TurmaId { get; set; }
    }
}




