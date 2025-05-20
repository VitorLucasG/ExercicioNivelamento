using Questao5.Domain.Entities;
using Questao5.Domain.RepositoryContracts.Command;
using Questao5.Infrastructure.Sqlite.DapperInfra;

namespace Questao5.Infrastructure.Database.Repositories.Command
{
    public class WriteMovimentoContaRepository : BaseRepository<MovimentoEntity>, IWriteMovimentoContaRepository
    {
        public WriteMovimentoContaRepository(IDapperContext context) : base(context) 
        {
        }
    }
}
