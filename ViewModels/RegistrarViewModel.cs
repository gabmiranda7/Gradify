using System.ComponentModel.DataAnnotations;

namespace Gradify.ViewModels
{
    public class RegistrarViewModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email em formato inválido.")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 40 caracteres.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmarSenha", ErrorMessage = "As senhas não coincidem.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        public string ConfirmarSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo de usuário é obrigatório.")]
        [Display(Name = "Tipo de Usuário")]
        public string Role { get; set; } = string.Empty;
    }
}