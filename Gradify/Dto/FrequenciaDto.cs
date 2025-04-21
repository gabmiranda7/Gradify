namespace Gradify.Dto
{
    public class FrequenciaCriacaoDto
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }
        public DateTime Data { get; set; }
    }

    public class FrequenciaLeituraDto
    {
        public int Id { get; set; }

        public int AlunoId { get; set; }
        public string AlunoNome { get; set; }

        public int TurmaId { get; set; }
        public string Materia { get; set; }
        public string DiaSemana { get; set; }
        public DateTime Data { get; set; }
    }
}