using Gradify.Data;
using Gradify.Dto;
using Gradify.Models;

namespace Gradify.Services.Professor
{
    public class ProfessorService : IProfessorInterface
    {
        private readonly AppDbContext _context;

        public ProfessorService(AppDbContext context)
        {
            _context = context;
        }

        public ProfessorLeituraDto Criar(ProfessorCriacaoDto dto)
        {
            var professor = new Models.Professor
            {
                Nome = dto.Nome
            };

            _context.Professores.Add(professor);
            _context.SaveChanges();

            return new ProfessorLeituraDto
            {
                Id = professor.Id,
                Nome = professor.Nome
            };
        }

        public IEnumerable<ProfessorLeituraDto> GetProfessores()
        {
            return _context.Professores
                .OrderBy(p => p.Nome)
                .Select(p => new ProfessorLeituraDto
                {
                    Id = p.Id,
                    Nome = p.Nome
                })
                .ToList();
        }

        public ProfessorLeituraDto ObterPorId(int id)
        {
            var professor = _context.Professores.Find(id);
            if (professor == null) return null;

            return new ProfessorLeituraDto
            {
                Id = professor.Id,
                Nome = professor.Nome
            };
        }

        public bool Excluir(int id)
        {
            var professor = _context.Professores.Find(id);
            if (professor == null) return false;

            _context.Professores.Remove(professor);
            _context.SaveChanges();
            return true;
        }

        public ProfessorLeituraDto Editar(int id, ProfessorCriacaoDto dto)
        {
            var professor = _context.Professores.Find(id);
            if (professor == null) return null;

            professor.Nome = dto.Nome;

            _context.Professores.Update(professor);
            _context.SaveChanges();

            return new ProfessorLeituraDto
            {
                Id = professor.Id,
                Nome = professor.Nome
            };
        }
    }
}