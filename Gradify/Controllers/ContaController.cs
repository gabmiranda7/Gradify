using Gradify.Data;
using Gradify.Enums;
using Gradify.Models;
using Gradify.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gradify.Controllers
{
    public class ContaController : Controller
    {
        private readonly SignInManager<Usuario> signInManager;
        private readonly UserManager<Usuario> userManager;
        private readonly AppDbContext context;

        public ContaController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, AppDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
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
                var usuario = await userManager.FindByEmailAsync(model.Email);

                if (usuario != null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(usuario, model.Senha, false);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(usuario, isPersistent: model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
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
                var usuario = new Usuario
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Tipo = model.Role == "Aluno" ? TipoUsuario.Aluno : TipoUsuario.Professor
                };

                var resultado = await userManager.CreateAsync(usuario, model.Senha);

                if (resultado.Succeeded)
                {
                    await userManager.AddToRoleAsync(usuario, model.Role);

                    if (model.Role == "Aluno")
                    {
                        var turmaPadrao = await context.Turmas.FirstOrDefaultAsync();

                        var aluno = new Aluno
                        {
                            Nome = model.Nome ?? "Novo Aluno",
                            Email = model.Email,
                            Matricula = Guid.NewGuid().ToString().Substring(0, 8),
                            UsuarioId = usuario.Id,
                            TurmaId = turmaPadrao?.Id
                        };

                        context.Alunos.Add(aluno);
                    }
                    else if (model.Role == "Professor")
                    {
                        var professor = new Professor
                        {
                            Nome = model.Nome ?? "Novo Professor",
                            Email = model.Email,
                            UsuarioId = usuario.Id
                        };

                        context.Professores.Add(professor);
                    }

                    await context.SaveChangesAsync();
                    return RedirectToAction("Login", "Conta");
                }
                else
                {
                    foreach (var erro in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, erro.Description);
                    }
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

                return RedirectToAction("MudarSenha", new { username = usuario.UserName });
            }

            return View(model);
        }

        public IActionResult MudarSenha(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerificarEmail");
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
                        if (resultado.Succeeded)
                        {
                            return RedirectToAction("Login", "Conta");
                        }
                    }

                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }

                ModelState.AddModelError("", "Email não encontrado!");
                return View(model);
            }

            ModelState.AddModelError("", "Algo deu errado, tente novamente.");
            return View(model);
        }

        [HttpPost]
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