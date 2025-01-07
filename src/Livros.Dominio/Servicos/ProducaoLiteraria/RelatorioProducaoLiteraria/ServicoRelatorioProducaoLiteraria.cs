using AutoMapper;
using LinqKit;
using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.Contratos.Servicos.ProducaoLiteraria;
using Livros.Dominio.ValueObjects;

namespace Livros.Dominio.Servicos.ProducaoLiteraria.RelatorioProducaoLiteraria;

public class ServicoRelatorioProducaoLiteraria : IServicoRelatorioProducaoLiteraria
{
    private readonly IRepositorioProducaoLiteraria _repositorioProducaoLiteraria;
    private readonly IMapper _mapper;

    public ServicoRelatorioProducaoLiteraria(IRepositorioProducaoLiteraria repositorioProducaoLiteraria, IMapper mapper)
    {
        _repositorioProducaoLiteraria = repositorioProducaoLiteraria;
        _mapper = mapper;
    }

    public async Task<ValueObjects.ProducaoLiteraria> ListarAsync()
    {
        var listaProducaoLiteraria = await _repositorioProducaoLiteraria.ListarProducaoLiteraria();

        List<ProducaoLiterariaItem> itens = new();

        listaProducaoLiteraria.ForEach(item =>
        {
            itens.Add(_mapper.Map<ProducaoLiterariaItem>(item));
        });

        ValueObjects.ProducaoLiteraria relatorioProducaoLiteraria = new();

        relatorioProducaoLiteraria.Itens = itens;

        return await Task.FromResult(relatorioProducaoLiteraria);
    }
}