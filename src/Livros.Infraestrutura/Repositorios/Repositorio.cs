﻿using Livros.Dominio.Contratos;
using Livros.Dominio.Entidades;
using Livros.Infraestrutura.BancoDados;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Livros.Infraestrutura.Repositorios;

public abstract class Repositorio<T> : IRepositorio<T> where T : Entidade
{
    protected LivroDbContext _database;
    protected DbSet<T> _dbSet;

    public Repositorio(LivroDbContext database)
    {
        _database = database;
        _dbSet = _database.Set<T>();
    }

    public virtual async Task<T?> BuscarPorIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public virtual async Task<T> CadastrarAsync(T entidade)
    {
        _dbSet.Add(entidade);

        await _database.SaveChangesAsync();

        return entidade;
    }

    public virtual async Task<T> EditarAsync(T entidade)
    {
        _database.Entry<T>(entidade).State = EntityState.Modified;

        var linhasAfetadas = await _database.SaveChangesAsync();

        if (linhasAfetadas <= 0)
        {
            return await Task.FromResult<T>(null);
        }

        return await Task.FromResult(entidade);
    }

    public virtual async Task<IEnumerable<T>> ListarAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> ListarAsync(
        Expression<Func<T, bool>> predicado, 
        int numeroPagina, 
        int tamanhoPagina,
        Expression<Func<T, object>>? campoOrdenacao = null,
        bool ordenacaoAscendente = true)
    {
        IQueryable<T> query = _dbSet.Where(predicado);

        if (campoOrdenacao != null)
        {
            query = ordenacaoAscendente
                ? query.OrderBy(campoOrdenacao)
                : query.OrderByDescending(campoOrdenacao);
        }

        return await query
            .Skip(numeroPagina * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicado)
    {
        return await _dbSet
           .Where(predicado)
           .CountAsync();
    }

    public virtual async Task<bool> RemoverAsync(T entidade)
    {
        _dbSet.Remove(entidade);

        var affectedRows = await _database.SaveChangesAsync();

        return (affectedRows >= 1);
    }
}