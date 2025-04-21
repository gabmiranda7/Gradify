using Gradify.Data;
using Gradify.Models;
using Gradify.Services.Aluno;
using Gradify.Services.Anotacao;
using Gradify.Services.Frequencia;
using Gradify.Services.Professor;
using Gradify.Services.Turma;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
builder.Services.AddScoped<IFrequenciaInterface, FrequenciaService>();
builder.Services.AddScoped<IProfessorInterface, ProfessorService>();
builder.Services.AddScoped<ITurmaInterface, TurmaService>();

var app = builder.Build();

var scope = app.Services.CreateScope();
await SeedData.Initialize(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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