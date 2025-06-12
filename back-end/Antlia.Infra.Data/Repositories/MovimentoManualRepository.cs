using Antlia.Domain.Entities;
using Antlia.Domain.Interfaces;
using DataAccessLayer.SqlServerAdapter;

namespace Antlia.Infra.Data.Repositories;

public class MovimentoManualRepository : IMovimentoManualRepository
{
    private readonly ISQLServerAdapter _sqlServerAdapter;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoCosifRepository _produtoCosifRepository;
    public MovimentoManualRepository(ISQLServerAdapter sqlServerAdapter,
                                     IProdutoRepository produtoRepository,
                                     IProdutoCosifRepository produtoCosifRepository)
    {
        _sqlServerAdapter = sqlServerAdapter;
        _produtoRepository = produtoRepository;
        _produtoCosifRepository = produtoCosifRepository;
    }

    public async Task<MovimentoManual> Create(MovimentoManual model)
    {
        _sqlServerAdapter.Open();
        var id = await _sqlServerAdapter.ExecuteScalarAsync("spMovimentoManualCreate", model);
        return await Get(int.Parse(id.ToString()));
    }

    public async Task<MovimentoManual> Get(int id)
    {
        var result = await Get(new MovimentoManual() { Id = id });
        return result.FirstOrDefault();
    }

    public async Task<List<MovimentoManual>> Get(MovimentoManual model)
    {
        _sqlServerAdapter.Open();
        var result = await _sqlServerAdapter.ExecuteReaderAsync<MovimentoManual>("spMovimentoManualSelect", model);
        foreach (var item in result)
        {
            item.Produto = await _produtoRepository.Get(item.ProdutoId.Value);
            item.ProdutoCosif = await _produtoCosifRepository.Get(item.ProdutoCosifId.Value);
        }
        return result;
    }

    public async Task Remove(int id)
    {
        _sqlServerAdapter.Open();
        await _sqlServerAdapter.ExecuteNonQueryAsync("spMovimentoManualRemove", new MovimentoManual() { Id = id });
    }

    public async Task Update(MovimentoManual model)
    {
        _sqlServerAdapter.Open();
        await _sqlServerAdapter.ExecuteNonQueryAsync("spMovimentoManualUpdate", model);
    }
}
