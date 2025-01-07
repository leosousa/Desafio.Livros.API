using AutoMapper;
using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Aplicacao.CasosUso.Assunto.Editar;
using Livros.Aplicacao.CasosUso.Assunto.Listar;
using Livros.Aplicacao.CasosUso.Autor.BuscarPorId;
using Livros.Aplicacao.CasosUso.Autor.Cadastrar;
using Livros.Aplicacao.CasosUso.Autor.Editar;
using Livros.Aplicacao.CasosUso.Autor.Listar;
using Livros.Aplicacao.CasosUso.Livro.BuscarPorId;
using Livros.Aplicacao.CasosUso.Livro.Cadastrar;
using Livros.Aplicacao.CasosUso.Livro.Editar;
using Livros.Aplicacao.CasosUso.Livro.Listar;
using Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiteraria;
using Livros.Dominio.DTOs;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.Dominio.Servicos.Autor.Listar;
using Livros.Dominio.Servicos.Livro.Listar;

namespace Livros.API.Config;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
        // Assunto
        CreateMap<AssuntoCadastroCommand, Assunto>();
        CreateMap<Assunto, AssuntoCadastroCommandResult>();

        CreateMap<Assunto, AssuntoBuscaPorIdQueryResult>();

        CreateMap<AssuntoListaPaginadaQuery, AssuntoListaFiltro>();
        CreateMap<ListaPaginadaResult<Assunto>, AssuntoListaPaginadaQueryResult>();
        CreateMap<Assunto, AssuntoItemResult>();

        CreateMap<AssuntoEdicaoCommand, Assunto>();
        CreateMap<Assunto, AssuntoEdicaoCommandResult>();


        // Autor
        CreateMap<AutorCadastroCommand, Autor>();
        CreateMap<Autor, AutorCadastroCommandResult>();

        CreateMap<Autor, AutorBuscaPorIdQueryResult>();

        CreateMap<AutorListaPaginadaQuery, AutorListaFiltro>();
        CreateMap<ListaPaginadaResult<Autor>, AutorListaPaginadaQueryResult>();
        CreateMap<Autor, AutorItemResult>();

        CreateMap<AutorEdicaoCommand, Autor>();
        CreateMap<Autor, AutorEdicaoCommandResult>();

        // Livro
        CreateMap<LivroCadastroCommand, Livro>()
            .ConstructUsing(src => 
                new Livro(
                    src.Titulo,
                    src.Editora,
                    src.Edicao,
                    src.AnoPublicacao,
                    null,
                    null
                ))
           .ForMember(dest => dest.Autores, opt => opt.Ignore()) // Ignora se você não quiser mapear as listas
           .ForMember(dest => dest.Assuntos, opt => opt.Ignore());

        CreateMap<Livro, LivroCadastroCommandResult>();
        CreateMap<Assunto, AssuntoResult>();
        CreateMap<Autor, AutorResult>();

        CreateMap<Livro, LivroBuscaPorIdQueryResult>();

        CreateMap<LivroListaPaginadaQuery, LivroListaFiltro>();
        CreateMap<ListaPaginadaResult<Livro>, LivroListaPaginadaQueryResult>();
        CreateMap<Livro, LivroItemResult>();

        CreateMap<LivroEdicaoCommand, Livro>()
            .ConstructUsing(src =>
                new Livro(
                    src.Titulo,
                    src.Editora,
                    src.Edicao,
                    src.AnoPublicacao,
                    null,
                    null
                ))
           .ForMember(dest => dest.Autores, opt => opt.Ignore()) // Ignora se você não quiser mapear as listas
           .ForMember(dest => dest.Assuntos, opt => opt.Ignore());

        CreateMap<Livro, LivroEdicaoCommandResult>();

        // Produção literária
        CreateMap<RelatorioProducaoLiterariaItem, Dominio.ValueObjects.ProducaoLiterariaItem>()
            .ForMember(dest => dest.Livro.Titulo, opt => opt.MapFrom(src => src.TituloLivro))
            .ForMember(dest => dest.Livro.AnoPublicacao, opt => opt.MapFrom(src => src.AnoPublicacao))
            .ForMember(dest => dest.Autor.Nome, opt => opt.MapFrom(src => src.NomeAutor))
            .ForMember(dest => dest.Assuntos, opt => opt.MapFrom(src => string.Join(", ", src.Assuntos)));

        CreateMap<Dominio.ValueObjects.ProducaoLiteraria, RelatorioProducaoLiterariaQueryResult>();
    }
}
