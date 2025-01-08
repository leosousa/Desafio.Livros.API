using MediatR;

namespace Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiterariaPdf;

public class RelatorioProducaoLiterariaPdfQuery : IRequest<RelatorioProducaoLiterariaPdfQueryResult>
{
    public Dominio.ValueObjects.ProducaoLiteraria Dados { get; set; }
}