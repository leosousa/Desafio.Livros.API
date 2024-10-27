using Livros.Dominio.Contratos;
using Livros.Infraestrutura.BancoDados;
using Livros.Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfraDependencyConfig
{
    public static void AdicionarDependenciasInfraestrutura(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LivroDbContext>(db =>
            db.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Singleton
        );

        services.AddScoped<IRepositorioAssunto, RepositorioAssunto>();
    }
}