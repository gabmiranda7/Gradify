using Gradify.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gradify.Services.Cursos
{
    public interface ICursoInterface
    {
        Task<List<CursoDTO>> GetCursos();
        Task<List<SelectListItem>> GetProfessores();
        Task<CursoDTO?> ObterPorId(int id);
        Task Criar(CursoDTO dto);
        Task Editar(CursoDTO dto);
        Task Excluir(int id);
    }
}