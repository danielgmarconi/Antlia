using Antlia.Domain.Entities;
using Antlia.Domain.Interfaces;
using DataAccessLayer.SqlServerAdapter;

namespace Antlia.Infra.Data.Repositories;

public class ProdutoCosifRepository : IProdutoCosifRepository
{
    private readonly ISQLServerAdapter _sqlServerAdapter;
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoCosifRepository(ISQLServerAdapter sqlServerAdapter,
                                  IProdutoRepository produtoRepository)
    {
        _sqlServerAdapter = sqlServerAdapter;
        _produtoRepository = produtoRepository;
    }

    public async Task<ProdutoCosif> Create(ProdutoCosif model)
    {
        _sqlServerAdapter.Open();
        var id = await _sqlServerAdapter.ExecuteScalarAsync("spProdutoCosifCreate", model);
        return await Get(int.Parse(id.ToString()));
    }

    public async Task<ProdutoCosif> Get(int id)
    {
        var result = await Get(new ProdutoCosif() { Id = id });
        return result.FirstOrDefault();
    }

    public async Task<List<ProdutoCosif>> Get(ProdutoCosif model)
    {
        _sqlServerAdapter.Open();
        var result = await _sqlServerAdapter.ExecuteReaderAsync<ProdutoCosif>("spProdutoCosifSelect", model);
        foreach (var item in result)
            item.Produto = await _produtoRepository.Get(item.ProdutoId.Value);
        return result;
    }

    public async Task Remove(int id)
    {
        _sqlServerAdapter.Open();
        await _sqlServerAdapter.ExecuteNonQueryAsync("spProdutoCosifRemove", new ProdutoCosif() { Id = id });
    }

    public async Task Update(ProdutoCosif model)
    {
        _sqlServerAdapter.Open();
        await _sqlServerAdapter.ExecuteNonQueryAsync("spProdutoCosifUpdate", model);
    }
}
