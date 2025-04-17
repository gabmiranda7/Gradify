using Gradify.Data;
using Gradify.Dto;
using Gradify.Models;

namespace Gradify.Services.Turma
{
    public class TurmaService : ITurmaInterface
    {
        private readonly AppDbContext _context;

        public TurmaService(AppDbContext context)
        {
            _context = context;
        }

        public TurmaLeituraDto Criar(TurmaCriacaoDto dto)
        {
            var turma = new Models.Turma
            {
                Materia = dto.Materia,
                DiaDaAula = dto.DiaDaAula,
                ProfessorId = dto.ProfessorId
            };

            _context.Turmas.Add(turma);
            _context.SaveChanges();

            return new TurmaLeituraDto
            {
                Id = turma.Id,
                Materia = turma.Materia,
                DiaDaAula = turma.DiaDaAula,
                ProfessorId = turma.ProfessorId
            };
        }

        public IEnumerable<TurmaLeituraDto> GetTurmas()
        {
            return _context.Turmas
                .Select(t => new TurmaLeituraDto
                {
                    Id = t.Id,
                    Materia = t.Materia,
                    DiaDaAula = t.DiaDaAula,
                    ProfessorId = t.ProfessorId,
                    ProfessorNome = t.Professor.Nome
                })
                .ToList();
        }

        public TurmaLeituraDto ObterPorId(int id)
        {
            var turma = _context.Turmas.Find(id);
            if (turma == null) return null;

            return new TurmaLeituraDto
            {
                Id = turma.Id,
                Materia = turma.Materia,
                DiaDaAula = turma.DiaDaAula,
                ProfessorId = turma.ProfessorId
            };
        }

        public bool Excluir(int id)
        {
            var turma = _context.Turmas.Find(id);
            if (turma == null) return false;

            _context.Turmas.Remove(turma);
            _context.SaveChanges();
            return true;
        }

        public TurmaLeituraDto Editar(int id, TurmaCriacaoDto dto)
        {
            var turma = _context.Turmas.Find(id);
            if (turma == null) return null;

            turma.Materia = dto.Materia;
            turma.DiaDaAula = dto.DiaDaAula;
            turma.ProfessorId = dto.ProfessorId;

            _context.Turmas.Update(turma);
            _context.SaveChanges();

            return new TurmaLeituraDto
            {
                Id = turma.Id,
                Materia = turma.Materia,
                DiaDaAula = turma.DiaDaAula,
                ProfessorId = turma.ProfessorId
            };
        }
    }
}