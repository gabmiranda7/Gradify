namespace Gradify.DTOs
{
    public class AlunoDto : UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;

        public ICollection<AnotacaoDto> Anotacoes { get; set; } = new List<AnotacaoDto>();
        public ICollection<AlunoTurmaDto> AlunosTurmas { get; set; } = new List<AlunoTurmaDto>();
        public ICollection<FrequenciaDto> Frequencias { get; set; } = new List<FrequenciaDto>();
        public ICollection<TurmaDto> Turmas { get; set; } = new List<TurmaDto>();
    }

}