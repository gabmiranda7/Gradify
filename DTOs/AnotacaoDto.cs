namespace Gradify.DTOs
{
    public class AnotacaoDto
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
    }
}