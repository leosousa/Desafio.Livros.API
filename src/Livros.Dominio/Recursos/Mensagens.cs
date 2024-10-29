using Livros.Dominio.Entidades;

namespace Livros.Dominio.Recursos;

public static class Mensagens
{
    public const string AssuntoNaoInformado = "Assunto não informado";
    public const string AssuntoECampoObrigatorio = "Assunto é campo obrigatório";
    public static string AssuntoPodeTerAteXCaracteres = $"Assunto precisa ter no máximo {Assunto.ASSUNTO_DESCRICAO_MAXIMO_CARACTERES.ToString()} caracteres";
    public const string CodigoAssuntoInvalido = "Código do assunto inválido";
    public const string AssuntoNaoEncontrado = "Assunto não encontrado";
    public const string CodigoAssuntoNaoInformado = "Identificador do assunto não informado";
    public const string AssuntoNaoEditado = "Assunto não editado";
    public const string OcorreuUmErroAoEditarAssunto = "Ocorreu um erro ao editar o assunto";
    public const string OcorreuUmErroAoCadastrarAssunto = "Ocorreu um erro ao cadastrar o assunto";
    public const string IdAssuntoNaoInformado = "Id do assunto não informado";
    public const string AssuntoNaoDeletado = "Assunto não deletado";

    public const string NomeECampoObrigatorio = "Nome do autor é campo obrigatório";
    public static string NomePodeTerAteXCaracteres = $"Nome do autor precisa ter no máximo {Autor.AUTOR_NOME_MAXIMO_CARACTERES.ToString()} caracteres";
    public const string CodigoAutorInvalido = "Código do autor inválido";
    public const string AutorNaoInformado = "Autor não informado";
    public const string OcorreuUmErroAoCadastrarAutor = "Ocorreu um erro ao cadastrar o autor";
    public const string AutorNaoEncontrado = "Autor não encontrado";
    public const string AutorNaoDeletado = "Autor não deletado";
    public const string OcorreuUmErroAoEditarAutor = "Ocorreu um erro ao editar o autor";
    public const string CodigoAutorNaoInformado = "Código do autor não informado";

    public const string LivroTituloECampoObrigatorio = "Título é campo obrigatório";
    public static string LivroTituloPodeTerAteXCaracteres = $"Título do livro precisa ter no máximo {Livro.TITULO_MAXIMO_CARACTERES.ToString()} caracteres";
    public const string LivroEditoraECampoObrigatorio = "Editora é campo obrigatório";
    public static string LivroEditoraPodeTerAteXCaracteres = $"Editora do livro precisa ter no máximo {Livro.EDITORA_MAXIMO_CARACTERES.ToString()} caracteres";
    public const string LivroEdicaoInvalido = "Edição inválida";
    public const string LivroAnoPublicacaoInvalido = "Ano de publicação inválido";
    public const string LivroAutorECampoObrigatorio = "Autor é campo obrigatório. Selecione pelo menos 1.";
    public const string LivroAssuntoECampoObrigatorio = "Assunto é campo obrigatório. Selecione pelo menos 1.";
    public const string CodigoLivroInvalido = "Identificador do livro inválido";
    public const string LivroNaoInformado = "Livro não informado";
    public const string OcorreuUmErroAoCadastrarLivro = "Ocorreu um erro ao cadastrar o livro";
    public const string LivroNaoEncontrado = "Livro não encontrado";
    public const string LivroNaoDeletado = "Livro não deletaado";
    public const string OcorreuUmErroAoEditarLivro = "Ocorreu um erro ao editar o livro";
}