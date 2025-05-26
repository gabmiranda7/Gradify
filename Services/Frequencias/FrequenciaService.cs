using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Frequencia
{
    public class FrequenciaService : IFrequenciaInterface
    {
        private readonly AppDbContext _context;

        public FrequenciaService(AppDbContext context)
        {
            _context = context;
        }

        public FrequenciaDto Criar(FrequenciaDto dto)
        {
            var turma = _context.Turmas.Find(dto.TurmaId);
            if (turma == null)
                return null;

            var frequencia = new Models.Frequencia
            {
                AlunoId = dto.AlunoId,
                TurmaId = dto.TurmaId,
                Data = DateTime.Now
            };

            _context.Frequencias.Add(frequencia);
            _context.SaveChanges();

            var aluno = _context.Alunos.Find(dto.AlunoId);

            return new FrequenciaDto
            {
                Id = frequencia.Id,
                AlunoId = frequencia.AlunoId,
                TurmaId = frequencia.TurmaId,
                Data = frequencia.Data,
                AlunoNome = aluno?.Nome,
                TurmaNome = turma?.Nome
            };
        }

        public IEnumerable<FrequenciaDto> GetFrequencias()
        {
            return _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .Select(f => new FrequenciaDto
                {
                    Id = f.Id,
                    AlunoId = f.AlunoId,
                    TurmaId = f.TurmaId,
                    Data = f.Data,
                    AlunoNome = f.Aluno.Nome,
                    TurmaNome = f.Turma.Nome
                })
                .ToList();
        }

        public FrequenciaDto ObterPorId(int id)
        {
            var frequencia = _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .FirstOrDefault(f => f.Id == id);

            if (frequencia == null) return null;

            return new FrequenciaDto
            {
                Id = frequencia.Id,
                AlunoId = frequencia.AlunoId,
                TurmaId = frequencia.TurmaId,
                Data = frequencia.Data,
                AlunoNome = frequencia.Aluno?.Nome,
                TurmaNome = frequencia.Turma?.Nome
            };
        }

        public bool Excluir(int id)
        {
            var frequencia = _context.Frequencias.Find(id);
            if (frequencia == null) return false;

            _context.Frequencias.Remove(frequencia);
            _context.SaveChanges();
            return true;
        }

        public FrequenciaDto Editar(int id, FrequenciaDto dto)
        {
            var frequencia = _context.Frequencias.Find(id);
            if (frequencia == null) return null;

            frequencia.AlunoId = dto.AlunoId;
            frequencia.TurmaId = dto.TurmaId;
            frequencia.Data = DateTime.Now;

            _context.Frequencias.Update(frequencia);
            _context.SaveChanges();

            var aluno = _context.Alunos.Find(frequencia.AlunoId);
            var turma = _context.Turmas.Find(frequencia.TurmaId);

            return new FrequenciaDto
            {
                Id = frequencia.Id,
                AlunoId = frequencia.AlunoId,
                TurmaId = frequencia.TurmaId,
                Data = frequencia.Data,
                AlunoNome = aluno?.Nome,
                TurmaNome = turma?.Nome
            };
        }

        public List<FrequenciaDto> BuscarFrequenciasPorAluno(int alunoId)
        {
            return _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .Where(f => f.AlunoId == alunoId)
                .Select(f => new FrequenciaDto
                {
                    Id = f.Id,
                    AlunoId = f.AlunoId,
                    TurmaId = f.TurmaId,
                    Data = f.Data,
                    AlunoNome = f.Aluno.Nome,
                    TurmaNome = f.Turma.Nome
                })
                .ToList();
        }
    }
}