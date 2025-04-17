namespace Gradify.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;

        public ICollection<Turma> Turmas { get; set; } = new List<Turma>();
    }
}