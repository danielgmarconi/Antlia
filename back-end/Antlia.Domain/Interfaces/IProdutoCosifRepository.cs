using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlia.Domain.Entities;

namespace Antlia.Domain.Interfaces
{
    public interface IProdutoCosifRepository
    {
        Task<ProdutoCosif> Get(int id);
        Task<List<ProdutoCosif>> Get(ProdutoCosif model);
        Task<ProdutoCosif> Create(ProdutoCosif model);
        Task Update(ProdutoCosif model);
        Task Remove(int id);
    }
}
