using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DominioDependencyConfig
{
    public static void AdicionarDependenciasDominio(this IServiceCollection services)
    {
        services.AddScoped<IServicoCadastroAssunto, ServicoCadastroAssunto>();

        services.AddValidatorsFromAssembly(Assembly.Load("Livros.Dominio"));
    }
}
