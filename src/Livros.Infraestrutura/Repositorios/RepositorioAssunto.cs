using Livros.Dominio.Contratos;
using Livros.Dominio.DTOs.Assunto;
using Livros.Dominio.Entidades;
using Livros.Infraestrutura.BancoDados;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Livros.Infraestrutura.Repositorios;

public class RepositorioAssunto : Repositorio<Assunto>, IRepositorioAssunto
{
    public RepositorioAssunto(LivroDbContext database) : base(database)
    {
    }

    public virtual async Task<IEnumerable<AssuntoComLivroDto>> ListarComLivrosAssociadosAsync(
        Expression<Func<Assunto, bool>> predicado,
        int numeroPagina,
        int tamanhoPagina,
        Expression<Func<Assunto, object>>? campoOrdenacao = null,
        bool ordenacaoAscendente = true)
    {
        IQueryable<Assunto> query = _dbSet.Where(predicado);

        if (campoOrdenacao != null)
        {
            query = ordenacaoAscendente
                ? query.OrderBy(campoOrdenacao)
                : query.OrderByDescending(campoOrdenacao);
        }

        // Projeção para incluir a quantidade de livros associados
        var resultado = query
            .Select(a => new AssuntoComLivroDto
            {
                Id = a.Id,
                Descricao = a.Descricao,
                PossuiLivrosAssociados = a.Livros.Any() // Propriedade de navegação
            });

        return await resultado
            .Skip(numeroPagina * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();
    }
}