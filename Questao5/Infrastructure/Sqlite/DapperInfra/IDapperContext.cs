using System.Data;

namespace Questao5.Infrastructure.Sqlite.DapperInfra
{
    public interface IDapperContext
    {
        IDbConnection GetConnection();
    }
}
