namespace Gradify.DTOs
{
    public class FrequenciaDTO
    {
        public int Id { get; set; }
        public DateTime DataFrequencia { get; set; }

        public int AlunoId { get; set; }
        public string? NomeAluno { get; set; }

        public int TurmaId { get; set; }
        public string? NomeTurma { get; set; }
    }
}