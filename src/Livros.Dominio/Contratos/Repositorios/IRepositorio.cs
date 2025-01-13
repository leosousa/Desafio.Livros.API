using Livros.Dominio.Entidades;
using System.Linq.Expressions;

namespace Livros.Dominio.Contratos;

public interface IRepositorio<T> where T : Entidade
{
    /// <summary>
    /// Busca um registro armazenado na base de dados pelo seu identificador
    /// </summary>
    /// <param name="id">Identificador do registro buscada</param>
    /// <returns>T encontrado</returns>
    Task<T?> BuscarPorIdAsync(int id);

    /// <summary>
    /// Lista os registros na base de dados de acordo com os filtros e paginação
    /// </summary>
    /// <param name="predicado">Expressão lambda de filtro de busca</param>
    /// <param name="numeroPagina">Número da página</param>
    /// <param name="tamanhoPagina">Tamanho da página</param>
    /// <param name="campoOrdenacao">Campo de ordenação</param>
    /// <param name="ordenacaoAscendente">Ordenação ascendente (true) ou descendente (false)</param>
    /// <returns>Lista de registros encontrados</returns>
    Task<IEnumerable<T>> ListarAsync(Expression<Func<T, bool>> predicado, 
        int numeroPagina, 
        int tamanhoPagina,
        Expression<Func<T, object>>? campoOrdenacao = null,
        bool ordenacaoAscendente = true);

    /// <summary>
    /// Conta os registros na base de dados de acordo com os filtros informados
    /// </summary>
    /// <param name="predicado">Expressão lambda de filtro de busca</param>
    /// <returns>Total de registros encontrados</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicado);

    /// <summary>
    /// Cadastra um novo registro na base de dados
    /// </summary>
    /// <param name="registro">Registro a ser cadastrado</param>
    /// <returns>T cadastrado</returns>
    Task<T> CadastrarAsync(T registro);

    /// <summary>
    /// Atualiza um registro no banco de dados
    /// </summary>
    /// <param name="registro">Registro a ser atualizado</param>
    /// <returns>T atualizado</returns>
    Task<T> EditarAsync(T registro);

    /// <summary>
    /// Remove um registro do banco de dados
    /// </summary>
    /// <param name="registro">Registro a ser removido</param>
    /// <returns>Retorna se o registro foi ou não removido</returns>
    Task<bool> RemoverAsync(T registro);
}