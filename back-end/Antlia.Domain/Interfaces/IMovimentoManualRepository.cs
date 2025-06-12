using Antlia.Domain.Entities;

namespace Antlia.Domain.Interfaces
{
    public interface IMovimentoManualRepository
    {
        Task<MovimentoManual> Get(int id);
        Task<List<MovimentoManual>> Get(MovimentoManual model);
        Task<MovimentoManual> Create(MovimentoManual model);
        Task Update(MovimentoManual model);
        Task Remove(int id);
    }
}
