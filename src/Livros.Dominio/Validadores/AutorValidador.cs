using FluentValidation;
using Livros.Dominio.Entidades;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Validadores;

public class AutorValidador : AbstractValidator<Autor>
{
    public AutorValidador()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
                .WithMessage(Mensagens.NomeECampoObrigatorio)
            .MaximumLength(20)
                .WithMessage(Mensagens.NomePodeTerAteXCaracteres);
    }
}