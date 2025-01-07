using FluentValidation;
using Livros.Dominio.Contratos;
using Livros.Dominio.Contratos.Servicos.Assunto;
using Livros.Dominio.Contratos.Servicos.Autor;
using Livros.Dominio.Contratos.Servicos.Livro;
using Livros.Dominio.Contratos.Servicos.ProducaoLiteraria;
using Livros.Dominio.Servicos.Assunto.BuscarPorId;
using Livros.Dominio.Servicos.Assunto.Cadastrar;
using Livros.Dominio.Servicos.Assunto.Deletar;
using Livros.Dominio.Servicos.Assunto.Editar;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.Dominio.Servicos.Autor.BuscarPorId;
using Livros.Dominio.Servicos.Autor.Cadastrar;
using Livros.Dominio.Servicos.Autor.Deletar;
using Livros.Dominio.Servicos.Autor.Editar;
using Livros.Dominio.Servicos.Autor.Listar;
using Livros.Dominio.Servicos.Livro.BuscarPorId;
using Livros.Dominio.Servicos.Livro.Cadastrar;
using Livros.Dominio.Servicos.Livro.Deletar;
using Livros.Dominio.Servicos.Livro.Editar;
using Livros.Dominio.Servicos.Livro.Listar;
using Livros.Dominio.Servicos.ProducaoLiteraria.RelatorioProducaoLiteraria;
using Livros.Dominio.Validadores;

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

        services.AddScoped<IServicoCadastroAutor, ServicoCadastroAutor>();
        services.AddScoped<IServicoBuscaAutorPorId, ServicoBuscaAutorPorId>();
        services.AddScoped<IServicoListagemAutor, ServicoListagemAutor>();
        services.AddScoped<IServicoEdicaoAutor, ServicoEdicaoAutor>();
        services.AddScoped<IServicoDelecaoAutor, ServicoDelecaoAutor>();

        services.AddScoped<IServicoCadastroLivro, ServicoCadastroLivro>();
        services.AddScoped<IServicoBuscaLivroPorId, ServicoBuscaLivroPorId>();
        services.AddScoped<IServicoListagemLivro, ServicoListagemLivro>();
        services.AddScoped<IServicoEdicaoLivro, ServicoEdicaoLivro>();
        services.AddScoped<IServicoDelecaoLivro, ServicoDelecaoLivro>();

        services.AddScoped<IServicoRelatorioProducaoLiteraria, ServicoRelatorioProducaoLiteraria>();

        //services.AddValidatorsFromAssembly(Assembly.Load("Livros.Dominio"));
        services.AddValidatorsFromAssemblyContaining<AssuntoValidador>();
        services.AddValidatorsFromAssemblyContaining<AutorValidador>();
        services.AddValidatorsFromAssemblyContaining<LivroValidador>();
    }
}
