using Questao5.Domain.Entities;
using Questao5.Domain.RepositoryContracts.Query;
using Questao5.Infrastructure.Sqlite.DapperInfra;

namespace Questao5.Infrastructure.Database.Repositories.Queries
{
    public class ReadSaldoContaRepository : BaseRepository<ContaCorrenteEntity>, IReadSaldoContaRepository
    {
        public ReadSaldoContaRepository(IDapperContext context) : base(context) 
        {
        }
    }
}
