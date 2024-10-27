using AutoMapper;
using Livros.Aplicacao.CasosUso.Assunto.Cadastrar;
using Livros.Dominio.Entidades;

namespace Livros.API.Config;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
        CreateMap<AssuntoCadastroCommand, Assunto>();
        CreateMap<Assunto, AssuntoCadastroCommandResult>();
    }
}
