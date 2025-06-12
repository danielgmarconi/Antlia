using Antlia.Domain.Entities;
using Antlia.Domain.Interfaces;
using DataAccessLayer.SqlServerAdapter;

namespace Antlia.Infra.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ISQLServerAdapter _sqlServerAdapter;
    public ProdutoRepository(ISQLServerAdapter sqlServerAdapter)
    {
        _sqlServerAdapter = sqlServerAdapter;
    }

    public async Task<Produto> Create(Produto model)
    {
        _sqlServerAdapter.Open();
        var id = await _sqlServerAdapter.ExecuteScalarAsync("spProdutoCreate", model);
        return await Get(int.Parse(id.ToString()));
    }

    public async Task<Produto> Get(int id)
    {
        var result = await Get(new Produto() { Id = id });
        return result.FirstOrDefault();
    }

    public async Task<List<Produto>> Get(Produto model)
    {
        _sqlServerAdapter.Open();
        return await _sqlServerAdapter.ExecuteReaderAsync<Produto>("spProdutoSelect", model);
    }

    public async Task Remove(int id)
    {
        _sqlServerAdapter.Open();
        await _sqlServerAdapter.ExecuteNonQueryAsync("spProdutoRemove", new Produto() { Id = id });
    }

    public async Task Update(Produto model)
    {
        _sqlServerAdapter.Open();
        await _sqlServerAdapter.ExecuteNonQueryAsync("spProdutoUpdate", model);
    }
}
