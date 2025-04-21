namespace Gradify.Dto
{
    public class TurmaCriacaoDto
    {
        public int Id { get; set; }
        public string Materia { get; set; } = string.Empty;
        public string DiaDaAula { get; set; } = string.Empty;
        public int ProfessorId { get; set; }
    }

    public class TurmaLeituraDto
    {
        public int Id { get; set; }
        public string Materia { get; set; } = string.Empty;
        public string DiaDaAula { get; set; } = string.Empty;
        public int ProfessorId { get; set; }
        public string ProfessorNome { get; set; } = string.Empty;
    }
}