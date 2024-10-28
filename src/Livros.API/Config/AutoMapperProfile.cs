using AutoMapper;
using Livros.Aplicacao.CasosUso.Assunto.BuscarPorId;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Aplicacao.CasosUso.Assunto.Editar;
using Livros.Aplicacao.CasosUso.Assunto.Listar;
using Livros.Dominio.DTOs;
using Livros.Dominio.Entidades;
using Livros.Dominio.Servicos.Assunto.Listar;

namespace Livros.API.Config;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
        CreateMap<AssuntoCadastroCommand, Assunto>();
        CreateMap<Assunto, AssuntoCadastroCommandResult>();

        CreateMap<Assunto, AssuntoBuscaPorIdQueryResult>();

        CreateMap<AssuntoListaPaginadaQuery, AssuntoListaFiltro>();
        CreateMap<ListaPaginadaResult<Assunto>, AssuntoListaPaginadaQueryResult>();
        CreateMap<Assunto, AssuntoItemResult>();

        CreateMap<AssuntoEdicaoCommand, Assunto>();
        CreateMap<Assunto, AssuntoEdicaoCommandResult>();
    }
}
