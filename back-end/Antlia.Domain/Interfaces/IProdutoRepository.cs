using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlia.Domain.Entities;

namespace Antlia.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto> Get(int id);
        Task<List<Produto>> Get(Produto model);
        Task<Produto> Create(Produto model);
        Task Update(Produto model);
        Task Remove(int id);
    }
}
