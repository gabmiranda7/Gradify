namespace Gradify.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public ICollection<Turma> Turmas { get; set; } = new List<Turma>();

    }
}