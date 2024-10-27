using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Livros.Infraestrutura.BancoDados;

public static class ServicoAtualizacaoBancoDados
{
    public static void UseServicoAtualizacaoBancoDados(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            serviceScope.ServiceProvider.GetService<LivroDbContext>()?.Database.Migrate();
        }
    }
}
