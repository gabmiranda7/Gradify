using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Frequencias
{
    public class FrequenciaService : IFrequenciaInterface
    {
        private readonly AppDbContext _context;

        public FrequenciaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FrequenciaDto>> GetFrequencias()
        {
            try
            {
                return await _context.Frequencias
                    .Select(f => new FrequenciaDto
                    {
                        Id = f.Id,
                        DataRegistro = f.DataRegistro,
                        AlunoId = f.AlunoId,
                        TurmaId = f.TurmaId
                    }).ToListAsync();
            }
            catch
            {
                return new List<FrequenciaDto>();
            }
        }

        public async Task<FrequenciaDto?> ObterPorId(int id)
        {
            try
            {
                var f = await _context.Frequencias.FindAsync(id);
                if (f == null) return null;

                return new FrequenciaDto
                {
                    Id = f.Id,
                    DataRegistro = f.DataRegistro,
                    AlunoId = f.AlunoId,
                    TurmaId = f.TurmaId
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<FrequenciaDto> Criar(FrequenciaDto dto)
        {
            var model = new Frequencia
            {
                DataRegistro = dto.DataRegistro,
                AlunoId = dto.AlunoId,
                TurmaId = dto.TurmaId
            };

            try
            {
                _context.Frequencias.Add(model);
                await _context.SaveChangesAsync();

                dto.Id = model.Id;
                return dto;
            }
            catch
            {
                return dto;
            }
        }

        public async Task<FrequenciaDto?> Editar(int id, FrequenciaDto dto)
        {
            try
            {
                var model = await _context.Frequencias.FindAsync(id);
                if (model == null) return null;

                model.DataRegistro = dto.DataRegistro;
                model.AlunoId = dto.AlunoId;
                model.TurmaId = dto.TurmaId;

                await _context.SaveChangesAsync();
                return dto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var model = await _context.Frequencias.FindAsync(id);
                if (model == null) return false;

                _context.Frequencias.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}