namespace Gradify.DTOs
{
    public class AnotacaoDTO
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }

        public int AlunoId { get; set; }
        public string? AlunoNome { get; set; }
        public string? CursoNome { get; set; }
        public int CursoId { get; set; }
    }
}