using AutoMapper;
using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Aplicacao.CasosUso.Assunto.Editar;
using Livros.Aplicacao.CasosUso.Assunto.Listar;
using Livros.Aplicacao.CasosUso.Autor.BuscarPorId;
using Livros.Aplicacao.CasosUso.Autor.Cadastrar;
using Livros.Aplicacao.CasosUso.Autor.Editar;
using Livros.Aplicacao.CasosUso.Autor.Listar;
using Livros.Dominio.DTOs;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Assunto.Listar;
using Livros.Dominio.Servicos.Autor.Listar;

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
    }
}
