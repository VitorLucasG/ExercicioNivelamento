using System.Data;
using Questao5.Domain.Entities;
using Questao5.Domain.RepositoryContracts.Query;
using Questao5.Infrastructure.Sqlite.DapperInfra;

namespace Questao5.Infrastructure.Database.Repositories.Queries
{
    public class ReadIdempotenciaRepository: BaseRepository<IdempotenciaEntity>, IReadIdempotenciaRepository
    {
        public ReadIdempotenciaRepository(IDapperContext context) : base(context)
        {
        }
    }
}
