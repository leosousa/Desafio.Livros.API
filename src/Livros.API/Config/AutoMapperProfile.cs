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
using Livros.Dominio.DTOs.Assunto;
using Livros.Dominio.DTOs.Autor;
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
        CreateMap<ListaPaginadaResult<AssuntoComLivroDto>, AssuntoListaPaginadaQueryResult>();
        CreateMap<Assunto, AssuntoItemResult>();
        CreateMap<AssuntoComLivroDto, AssuntoItemResult>();

        CreateMap<AssuntoEdicaoCommand, Assunto>();
        CreateMap<Assunto, AssuntoEdicaoCommandResult>();


        // Autor
        CreateMap<AutorCadastroCommand, Autor>();
        CreateMap<Autor, AutorCadastroCommandResult>();

        CreateMap<Autor, AutorBuscaPorIdQueryResult>();

        CreateMap<AutorListaPaginadaQuery, AutorListaFiltro>();
        CreateMap<ListaPaginadaResult<Autor>, AutorListaPaginadaQueryResult>();
        CreateMap<ListaPaginadaResult<AutorComLivroDto>, AutorListaPaginadaQueryResult>();
        CreateMap<Autor, AutorItemResult>();
        CreateMap<AutorComLivroDto, AutorItemResult>();

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
           .ForMember(dest => dest.Assuntos, opt => opt.Ignore())
           .ForMember(dest => dest.LocaisVenda, opt => opt.Ignore());

        CreateMap<Livro, LivroCadastroCommandResult>();
        CreateMap<Assunto, AssuntoResult>();
        CreateMap<Autor, AutorResult>();
        CreateMap<LivroLocalVenda, LocalVendaResult>()
            .ForMember(dest => dest.IdLocalVenda, opt => opt.MapFrom(src => src.LocalVenda.Id))
            .ForMember(dest => dest.LocalVenda, opt => opt.MapFrom(src => src.LocalVenda.Descricao))
            .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Valor));

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
           .ForMember(dest => dest.Assuntos, opt => opt.Ignore())
           .ForMember(dest => dest.LocaisVenda, opt => opt.Ignore());

        CreateMap<Livro, LivroEdicaoCommandResult>();

        // Produção literária
        CreateMap<RelatorioProducaoLiterariaItem, Dominio.ValueObjects.ProducaoLiterariaItem>()
            .ForPath(dest => dest.Livro.Titulo, opt => opt.MapFrom(src => src.TituloLivro))
            .ForPath(dest => dest.Livro.AnoPublicacao, opt => opt.MapFrom(src => src.AnoPublicacao))
            .ForPath(dest => dest.Autor.Nome, opt => opt.MapFrom(src => src.NomeAutor))
            .ForMember(dest => dest.Assuntos, opt => opt.MapFrom(src =>
                src.Assuntos.Split(new[] { ',' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            ));

        CreateMap<Dominio.ValueObjects.ProducaoLiteraria, RelatorioProducaoLiterariaQueryResult>()
            .ForMember(dest => dest.Relatorio, opt => opt.MapFrom(src => src));
    }
}
