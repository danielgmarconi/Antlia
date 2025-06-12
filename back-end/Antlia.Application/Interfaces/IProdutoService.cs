using Antlia.Application.Common;
using Antlia.Application.DTOs;

namespace Antlia.Application.Interfaces;

public interface IProdutoService
{
    Task<MethodResponse> Get(int id);
    Task<MethodResponse> Get(ProdutoDTO model);
    Task<MethodResponse> Create(ProdutoDTO model);
    Task<MethodResponse> Update(ProdutoDTO model);
    Task<MethodResponse> Remove(int id);
}
