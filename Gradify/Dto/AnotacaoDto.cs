namespace Gradify.Dto
{
    public class AnotacaoCriacaoDto
    {
        public int TurmaId { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }

    public class AnotacaoLeituraDto
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public string Materia { get; set; }
        public DateTime DataCriacao { get; set; }
        public int TurmaId { get; set; }
    }
}