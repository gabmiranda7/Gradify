using Microsoft.AspNetCore.Identity;

namespace Gradify.Models
{
    public class Usuario : IdentityUser
    {
        public string NomeCompleto { get; set; } = string.Empty;
    }
}