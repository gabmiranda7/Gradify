using System.ComponentModel.DataAnnotations;

namespace Gradify.ViewModels
{
    public class VerificarEmailViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
