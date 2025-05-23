using Gradify.Data;
using Gradify.Services.Alunos;
using Gradify.Services.Anotacoes;
using Gradify.Services.Frequencias;
using Gradify.Services.Turmas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAlunoInterface, AlunoService>();
builder.Services.AddScoped<IAnotacaoInterface, AnotacaoService>();
builder.Services.AddScoped<IFrequenciaInterface, FrequenciaService>();
builder.Services.AddScoped<ITurmaInterface, TurmaService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure o pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();