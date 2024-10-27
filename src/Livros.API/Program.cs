using Livros.API.Config;
using Livros.Infraestrutura.BancoDados;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AdicionarDependenciasDominio();

builder.Services.AdicionarDependenciasInfraestrutura(builder.Configuration);

var assemblies = new Assembly[] {
    Assembly.Load("Livros.Dominio"),
    Assembly.Load("Livros.Infraestrutura"),
    Assembly.Load("Livros.Aplicacao")
};

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseServicoAtualizacaoBancoDados();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
