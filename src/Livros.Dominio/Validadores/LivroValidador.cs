using FluentValidation;
using Livros.Dominio.Entidades;
using Livros.Dominio.Recursos;

namespace Livros.Dominio.Validadores;

public class LivroValidador : AbstractValidator<Livro>
{
    public LivroValidador()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty()
                .WithMessage(Mensagens.LivroTituloECampoObrigatorio)
            .MaximumLength(Livro.TITULO_MAXIMO_CARACTERES)
                .WithMessage(Mensagens.LivroTituloPodeTerAteXCaracteres);

        RuleFor(x => x.Editora)
            .NotEmpty()
                .WithMessage(Mensagens.LivroEditoraECampoObrigatorio)
            .MaximumLength(Livro.EDITORA_MAXIMO_CARACTERES)
                .WithMessage(Mensagens.LivroEditoraPodeTerAteXCaracteres);

        RuleFor(x => x.Edicao)
            .GreaterThanOrEqualTo(1)
                .WithMessage(Mensagens.LivroEdicaoInvalido);

        RuleFor(x => x.AnoPublicacao)
            .GreaterThanOrEqualTo(1)
                .WithMessage(Mensagens.LivroAnoPublicacaoInvalido);

        RuleFor(x => x.Autores)
            .NotEmpty()
                .WithMessage(Mensagens.LivroAutorECampoObrigatorio);

        RuleFor(x => x.Assuntos)
            .NotEmpty()
                .WithMessage(Mensagens.LivroAssuntoECampoObrigatorio);
    }
}