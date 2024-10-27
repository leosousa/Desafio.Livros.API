using FluentValidation;
using Livros.Dominio.Entidades;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Validadores;

public class AssuntoValidador : AbstractValidator<Assunto>
{
    public AssuntoValidador()
    {
        RuleFor(x => x.Descricao)
            .NotEmpty()
                .WithMessage(Mensagens.AssuntoECampoObrigatorio)
            .MaximumLength(20)
                .WithMessage(Mensagens.AssuntoPodeTerAteXCaracteres);
    }
}
