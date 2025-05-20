using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Exceptions;
using Questao5.Infrastructure.Database.Repositories.Helpers;

namespace Questao5.Infrastructure.Sqlite.DapperInfra
{
    public class DapperContext : IDapperContext
    {

        private readonly DatabaseConfig databaseConfig;
        public DapperContext(DatabaseConfig config) 
        { 
            this.databaseConfig = config;
        }

        public IDbConnection GetConnection()
        {
            IDbConnection conn = new SqliteConnection(databaseConfig.Name); ;
            try
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                SqlMapper.AddTypeHandler(new DateTimeHandler());
                SqlMapper.AddTypeHandler(new GuidHandler());
                SqlMapper.AddTypeHandler(new TimeSpanHandler());
                conn.Open();
            }
            catch(Exception ex) 
            {
                throw new GenericException("Erro ao abrir conexão com o banco", ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
            return conn;
        }

    }
}
