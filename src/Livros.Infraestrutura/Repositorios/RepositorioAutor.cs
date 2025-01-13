using Livros.Dominio.Contratos.Repositorios;
using Livros.Dominio.DTOs.Assunto;
using Livros.Dominio.DTOs.Autor;
using Livros.Dominio.Entidades;
using Livros.Infraestrutura.BancoDados;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Livros.Infraestrutura.Repositorios;

public class RepositorioAutor : Repositorio<Autor>, IRepositorioAutor
{
    public RepositorioAutor(LivroDbContext database) : base(database)
    {
    }

    public virtual async Task<IEnumerable<AutorComLivroDto>> ListarComLivrosAssociadosAsync(
        Expression<Func<Autor, bool>> predicado,
        int numeroPagina,
        int tamanhoPagina,
        Expression<Func<Autor, object>>? campoOrdenacao = null,
        bool ordenacaoAscendente = true)
    {
        IQueryable<Autor> query = _dbSet.Where(predicado);

        if (campoOrdenacao != null)
        {
            query = ordenacaoAscendente
                ? query.OrderBy(campoOrdenacao)
                : query.OrderByDescending(campoOrdenacao);
        }

        // Projeção para incluir a quantidade de livros associados
        var resultado = query
            .Select(a => new AutorComLivroDto
            {
                Id = a.Id,
                Nome = a.Nome,
                QuantidadeLivros = a.Livros.Count() // Propriedade de navegação
            });

        return await resultado
            .Skip(numeroPagina * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();
    }
}