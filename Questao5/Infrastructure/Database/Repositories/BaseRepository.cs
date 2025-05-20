using Dapper;
using Questao5.Domain.Exceptions;
using Questao5.Domain.RepositoryContracts;
using Questao5.Infrastructure.Database.Repositories.Helpers;
using Questao5.Infrastructure.Sqlite.DapperInfra;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;

namespace Questao5.Infrastructure.Database.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private IDbConnection _connection;

        public BaseRepository(IDapperContext dataContext)
        {
            _connection = dataContext.GetConnection();
        }

        public bool Add(TEntity entity)
        {
            int rowsAffected = 0;
            try
            {
                string tableName = GetTableName();
                string columns = GetColumns();
                string properties = GetPropertyNames();
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

                rowsAffected = _connection.Execute(query, entity);
            }
            catch (Exception ex)
            {
                throw new GenericException($"Erro ao incluir o registro na tabela {GetTableName()}", ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                _connection.Close();
            }

            return rowsAffected > 0 ? true : false;
        }

        public bool Delete(TEntity entity)
        {
            int rowsAffected = 0;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string keyProperty = GetKeyPropertyName();
                string query = $"DELETE FROM {tableName} WHERE {keyColumn} = @{keyProperty}";

                rowsAffected = _connection.Execute(query, entity);
            }
            catch (Exception ex)
            {
                throw new GenericException($"Erro ao consultar a tabela {GetTableName()}", ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                _connection.Close();
            }

            return rowsAffected > 0 ? true : false;
        }

        public IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> result = null;
            try
            {
                string tableName = GetTableName();
                string query = $"SELECT * FROM {tableName}";

                result = _connection.Query<TEntity>(query);
            }
            catch (Exception ex)
            {
                throw new GenericException($"Erro ao consultar a tabela {GetTableName()}", ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public TEntity GetById(string Id)
        {
            IEnumerable<TEntity> result = null;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string query = $"SELECT * FROM {tableName} WHERE {keyColumn} = '{Id}'";

                result = _connection.Query<TEntity>(query);
            }
            catch (Exception ex)
            {
                throw new GenericException($"Erro ao consultar a tabela {GetTableName()}", ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                _connection.Close();
            }

            return result.FirstOrDefault();
        }

        public bool Update(TEntity entity)
        {
            int rowsEffected = 0;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string keyProperty = GetKeyPropertyName();

                StringBuilder query = new StringBuilder();
                query.Append($"UPDATE {tableName} SET ");

                foreach (var property in GetProperties(true))
                {
                    var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

                    string propertyName = property.Name;
                    string columnName = columnAttr.Name;

                    query.Append($"{columnName} = @{propertyName},");
                }

                query.Remove(query.Length - 1, 1);

                query.Append($" WHERE {keyColumn} = @{keyProperty}");

                rowsEffected = _connection.Execute(query.ToString(), entity);
            }
            catch (Exception ex)
            {
                throw new GenericException($"Erro ao consultar a tabela {GetTableName()}", ex.Message, System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {
                _connection.Close();
            }

            return rowsEffected > 0 ? true : false;
        }

        private string GetTableName()
        {
            string tableName = "";
            var type = typeof(TEntity);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            if (tableAttr != null)
            {
                tableName = tableAttr.Name;
                return tableName;
            }

            return type.Name + "s";
        }

        public static string GetKeyColumnName()
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object[] keyAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                if (keyAttributes != null && keyAttributes.Length > 0)
                {
                    object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                    if (columnAttributes != null && columnAttributes.Length > 0)
                    {
                        ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                        return columnAttribute.Name;
                    }
                    else
                    {
                        return property.Name;
                    }
                }
            }

            return null;
        }


        private string GetColumns(bool excludeKey = false)
        {
            var type = typeof(TEntity);
            var columns = string.Join(", ", type.GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(ColumnAttribute)))
                .Select(p =>
                {
                    var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttr != null ? columnAttr.Name : p.Name;
                }));

            return columns;
        }

        protected string GetPropertyNames(bool excludeKey = false)
        {
            var properties = typeof(TEntity).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<ColumnAttribute>() == null);

            var values = string.Join(", ", properties.Select(p =>
            {
                return $"@{p.Name}";
            }));

            return values;
        }

        protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            var properties = typeof(TEntity).GetProperties()
                .Where(p => !excludeKey || p.GetCustomAttribute<ColumnAttribute>() == null);

            return properties;
        }

        protected string GetKeyPropertyName()
        {
            var properties = typeof(TEntity).GetProperties()
                .Where(p => p.GetCustomAttribute<ColumnAttribute>() != null);

            if (properties.Any())
            {
                return properties.FirstOrDefault().Name;
            }

            return null;
        }
    }
}
