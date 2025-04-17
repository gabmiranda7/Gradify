using Gradify.Data;
using Gradify.Dto;
using Gradify.Models;

namespace Gradify.Services.Aluno
{
    public class AlunoService : IAlunoInterface
    {
        private readonly AppDbContext _context;

        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public AlunoLeituraDto Criar(AlunoCriacaoDto dto)
        {
            var aluno = new Models.Aluno
            {
                Nome = dto.Nome,
                Matricula = dto.Matricula
            };

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return new AlunoLeituraDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Matricula = aluno.Matricula
            };
        }

        public IEnumerable<AlunoLeituraDto> GetAlunos()
        {
            return _context.Alunos
                .OrderBy(a => a.Nome)
                .Select(a => new AlunoLeituraDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Matricula = a.Matricula
                })
                .ToList();
        }

        public AlunoLeituraDto ObterPorId(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null) return null;

            return new AlunoLeituraDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Matricula = aluno.Matricula
            };
        }

        public bool Excluir(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null) return false;

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
            return true;
        }

        public AlunoLeituraDto Editar(int id, AlunoCriacaoDto dto)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null) return null;

            aluno.Nome = dto.Nome;
            aluno.Matricula = dto.Matricula;

            _context.Alunos.Update(aluno);
            _context.SaveChanges();

            return new AlunoLeituraDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Matricula = aluno.Matricula
            };
        }
    }
}
