using Gradify.Models;
using Gradify.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gradify.Controllers
{
    public class ContaController : Controller
    {
        private readonly SignInManager<Usuario> signInManager;
        private readonly UserManager<Usuario> userManager;

        public ContaController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultado = await signInManager.PasswordSignInAsync(model.Email, model.Senha, model.RememberMe, false);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario
                {
                    NomeCompleto = model.Nome,
                    Email = model.Email,
                    UserName = model.Email
                };

                var resultado = await userManager.CreateAsync(usuario, model.Senha);

                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(usuario, model.Role);
                    return RedirectToAction("Login", "Conta");
                }
                else
                {
                    foreach (var erro in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, erro.Description);
                    }

                    return View(model);
                }
            }

            return View(model);
        }

        public IActionResult VerificarEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerificarEmail(VerificarEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await userManager.FindByNameAsync(model.Email);
                if (usuario == null)
                {
                    ModelState.AddModelError(nameof(model.Email), "Nenhum usuário encontrado com esse email.");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("MudarSenha", "Conta", new { username = usuario.UserName });
                }
            }

            return View(model);
        }

        public IActionResult MudarSenha(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerificarEmail", "Conta");
            }
            return View(new MudarSenhaViewModel { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> MudarSenha(MudarSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await userManager.FindByNameAsync(model.Email);
                if (usuario != null)
                {
                    var resultado = await userManager.RemovePasswordAsync(usuario);
                    if (resultado.Succeeded)
                    {
                        resultado = await userManager.AddPasswordAsync(usuario, model.NovaSenha);
                        return RedirectToAction("Login", "Conta");
                    }
                    else
                    {
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email não encontrado!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Algo deu errado, tente novamente.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}