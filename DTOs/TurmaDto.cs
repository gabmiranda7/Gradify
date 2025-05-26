namespace Gradify.DTOs
{
    public class TurmaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public ICollection<AlunoTurmaDto> AlunosTurmas { get; set; } = new List<AlunoTurmaDto>();
        public ICollection<AnotacaoDto> Anotacoes { get; set; } = new List<AnotacaoDto>();
        public ICollection<AulaDto> Aulas { get; set; } = new List<AulaDto>();
        public ICollection<FrequenciaDto> Frequencias { get; set; } = new List<FrequenciaDto>();
        public ICollection<TurmaCursoDto> TurmasCursos { get; set; } = new List<TurmaCursoDto>();
    }
}