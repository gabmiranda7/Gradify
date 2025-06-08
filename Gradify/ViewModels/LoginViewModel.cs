using System.ComponentModel.DataAnnotations;

namespace Gradify.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty;

        [Display(Name = "Manter logado")]
        public bool RememberMe { get; set; }
    }
}