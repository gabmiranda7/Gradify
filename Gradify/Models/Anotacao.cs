using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Gradify.Models
{
    public class Anotacao
    {
        public int Id { get; set; }

        [Required]
        public string Comentario { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public int TurmaId { get; set; }
        public Turma Turma { get; set; } 
    }
}