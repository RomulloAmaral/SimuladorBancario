using Microsoft.EntityFrameworkCore;
using SimuladorBancario.Application.Interfaces;
using SimuladorBancario.Application.Services;
using SimuladorBancario.Infrastructure.Data;
using SimuladorBancario.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ===== 1. CONFIGURAÇÃO DO BANCO DE DADOS =====
builder.Services.AddDbContext<BancoContext>(opt =>
    opt.UseSqlite("Data Source=banco.db"));

// ===== 2. INJEÇÃO DE DEPENDÊNCIA =====
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IContaService, ContaService>();
builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();

// ===== 3. CONTROLLERS E SWAGGER =====
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== 4. DEVE FICAR AQUI (ANTES DO BUILD!) =====
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// ===== 5. CONSTRUIR A APLICAÇÃO =====
var app = builder.Build(); 

// ===== 6. APLICAR MIGRATIONS 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BancoContext>();
    db.Database.Migrate();
}


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("PermitirTudo"); 
app.UseHttpsRedirection();
app.MapControllers();

app.Run();