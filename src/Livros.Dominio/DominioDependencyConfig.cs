using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Servicos.Assunto.BuscarPorId;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using Livros.Dominio.Servicos.Assunto.Deletar;
using Livros.Dominio.Servicos.Assunto.Editar;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.Dominio.Validadores;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DominioDependencyConfig
{
    public static void AdicionarDependenciasDominio(this IServiceCollection services)
    {
        services.AddScoped<IServicoCadastroAssunto, ServicoCadastroAssunto>();
        services.AddScoped<IServicoBuscaAssuntoPorId, ServicoBuscaAssuntoPorId>();
        services.AddScoped<IServicoListagemAssunto, ServicoListagemAssunto>();
        services.AddScoped<IServicoEdicaoAssunto, ServicoEdicaoAssunto>();
        services.AddScoped<IServicoDelecaoAssunto, ServicoDelecaoAssunto>();

        //services.AddValidatorsFromAssembly(Assembly.Load("Livros.Dominio"));
        services.AddValidatorsFromAssemblyContaining<AssuntoValidador>();
    }
}
