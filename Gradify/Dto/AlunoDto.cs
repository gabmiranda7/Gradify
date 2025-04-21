using System.ComponentModel.DataAnnotations;

namespace Gradify.Dto
{
    public class AlunoCriacaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
    }

    public class AlunoLeituraDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public List<FrequenciaLeituraDto>? Frequencias { get; set; }
    }
}