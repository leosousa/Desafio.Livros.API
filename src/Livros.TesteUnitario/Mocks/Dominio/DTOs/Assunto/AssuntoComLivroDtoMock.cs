using Bogus;
using Livros.Dominio.DTOs.Assunto;

namespace Livros.TesteUnitario.Mocks.Dominio.DTOs;

public class AssuntoComLivroDtoMock : Faker<AssuntoComLivroDto>
{
    public AssuntoComLivroDtoMock() : base("pt_BR")
    {
        RuleFor(dto => dto.Id, faker => faker.Random.Int(min: 1, max: 100));

        RuleFor(dto => dto.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(dto => dto.PossuiLivrosAssociados, faker => faker.Random.Bool());
    }

    public static AssuntoComLivroDto GerarObjeto()
    {
        return new AssuntoComLivroDtoMock().Generate();
    }

    public static List<AssuntoComLivroDto> GerarObjetoLista(int quantidade = 10)
    {
        var lista = new List<AssuntoComLivroDto>();

        for (var index = 0; index < quantidade; index++)
        {
            lista.Add(GerarObjeto());
        }

        return lista;
    }
}