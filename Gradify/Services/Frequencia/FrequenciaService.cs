using Gradify.Data;
using Gradify.Dto;
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

        public FrequenciaLeituraDto Criar(FrequenciaCriacaoDto dto)
        {
            var turma = _context.Turmas.Find(dto.TurmaId);
            if (turma == null)
                return null;

            var diaAtual = DateTime.Now.DayOfWeek.ToString();
            var diaTraduzido = TraduzirDiaParaPortugues(diaAtual);

            if (turma.DiaDaAula != diaTraduzido)
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

            return new FrequenciaLeituraDto
            {
                Id = frequencia.Id,
                AlunoId = frequencia.AlunoId,
                TurmaId = frequencia.TurmaId,
                Data = frequencia.Data,
                AlunoNome = aluno?.Nome,
                Materia = turma.Materia,
                DiaSemana = turma.DiaDaAula
            };
        }

        private string TraduzirDiaParaPortugues(string diaIngles)
        {
            return diaIngles switch
            {
                "Monday" => "Segunda",
                "Tuesday" => "Terça",
                "Wednesday" => "Quarta",
                "Thursday" => "Quinta",
                "Friday" => "Sexta",
                "Saturday" => "Sábado",
                "Sunday" => "Domingo",
                _ => diaIngles
            };
        }

        public IEnumerable<FrequenciaLeituraDto> GetFrequencias()
        {
            return _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .Select(f => new FrequenciaLeituraDto
                {
                    Id = f.Id,
                    AlunoId = f.AlunoId,
                    TurmaId = f.TurmaId,
                    Data = f.Data,
                    AlunoNome = f.Aluno.Nome,
                    Materia = f.Turma.Materia,
                    DiaSemana = f.Turma.DiaDaAula
                })
                .ToList();
        }

        public FrequenciaLeituraDto ObterPorId(int id)
        {
            var frequencia = _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .FirstOrDefault(f => f.Id == id);

            if (frequencia == null) return null;

            return new FrequenciaLeituraDto
            {
                Id = frequencia.Id,
                AlunoId = frequencia.AlunoId,
                TurmaId = frequencia.TurmaId,
                Data = frequencia.Data,
                AlunoNome = frequencia.Aluno?.Nome,
                Materia = frequencia.Turma?.Materia,
                DiaSemana = frequencia.Turma?.DiaDaAula
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

        public FrequenciaLeituraDto Editar(int id, FrequenciaCriacaoDto dto)
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

            return new FrequenciaLeituraDto
            {
                Id = frequencia.Id,
                AlunoId = frequencia.AlunoId,
                TurmaId = frequencia.TurmaId,
                Data = frequencia.Data,
                AlunoNome = aluno?.Nome,
                Materia = turma?.Materia,
                DiaSemana = turma?.DiaDaAula
            };
        }
    }
}
