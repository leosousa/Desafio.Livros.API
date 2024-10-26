
using Bogus;

namespace Livros.TesteUnitario.Mocks.Dominio;

public class ValidationResultMock : Faker<FluentValidation.Results.ValidationResult>
{
    private ValidationResultMock() : base("pt_BR")
    {
    }

    private ValidationResultMock(string nomePropriedade, string mensagemErro) : base("pt_BR")
    {
        RuleFor(validation => validation.Errors, new List<FluentValidation.Results.ValidationFailure>
        {
            new FluentValidation.Results.ValidationFailure(nomePropriedade, mensagemErro)
        });
    }

    public static FluentValidation.Results.ValidationResult GerarObjetoValido()
    {
        return new ValidationResultMock().Generate();
    }

    public static FluentValidation.Results.ValidationResult GerarObjetoInvalido()
    {
        var faker = new Faker();

        var nomePropriedade = faker.Random.String(length: 40);
        var mensagemErro = faker.Random.String(length: 255);

        return new ValidationResultMock(nomePropriedade, mensagemErro).Generate();
    }

    public static FluentValidation.Results.ValidationResult GerarObjetoInvalido(string nomePropriedade, string mensagemErro)
    {
        return new ValidationResultMock(nomePropriedade, mensagemErro).Generate();
    }
}