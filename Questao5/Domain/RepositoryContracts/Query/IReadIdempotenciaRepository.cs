using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Repositories;

namespace Questao5.Domain.RepositoryContracts.Query
{
    public interface IReadIdempotenciaRepository: IBaseRepository<IdempotenciaEntity>
    {
    }
}
