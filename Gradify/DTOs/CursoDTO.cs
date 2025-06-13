namespace Gradify.DTOs
{
    public class CursoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<int> TurmaIds { get; set; } = new List<int>();
        public List<string> TurmaNomes { get; set; } = new List<string>();
    }
}