using Gradify.Data;
using Gradify.Models;
using Gradify.Services.Alunos;
using Gradify.Services.Anotacoes;
using Gradify.Services.Aulas;
using Gradify.Services.Cursos;
using Gradify.Services.Frequencias;
using Gradify.Services.Professores;
using Gradify.Services.Turmas;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Conta/AcessoNegado";
    options.LoginPath = "/Conta/Login";
});

builder.Services.AddScoped<IAlunoInterface, AlunoService>();
builder.Services.AddScoped<IAnotacaoInterface, AnotacaoService>();
//builder.Services.AddScoped<IAulaInterface, AulaService>();
builder.Services.AddScoped<ICursoInterface, CursoService>();
builder.Services.AddScoped<IFrequenciaInterface, FrequenciaService>();
builder.Services.AddScoped<IProfessorInterface, ProfessorService>();
builder.Services.AddScoped<ITurmaInterface, TurmaService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await SeedData.Initialize(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();