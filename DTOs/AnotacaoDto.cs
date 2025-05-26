namespace Gradify.DTOs
{
    public class AnotacaoDto
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}