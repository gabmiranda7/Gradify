using System.ComponentModel.DataAnnotations;

namespace Gradify.ViewModels
{
    public class MudarSenhaViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 40 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        [Compare("ConfirmarNovaSenha", ErrorMessage = "As senhas não coincidem.")]
        public string NovaSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nova Senha")]
        public string ConfirmarNovaSenha { get; set; } = string.Empty;
    }
}
