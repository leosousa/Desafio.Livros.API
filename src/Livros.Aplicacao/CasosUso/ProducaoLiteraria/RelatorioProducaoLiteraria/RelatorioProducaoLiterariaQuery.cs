using Livros.Aplicacao.DTOs;
using MediatR;

namespace Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiteraria;

public class RelatorioProducaoLiterariaQuery : IRequest<Result<RelatorioProducaoLiterariaQueryResult>>
{
}