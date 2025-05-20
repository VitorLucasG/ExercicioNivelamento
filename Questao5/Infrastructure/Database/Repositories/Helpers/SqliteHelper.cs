using Dapper;
using System.Data;

namespace Questao5.Infrastructure.Database.Repositories.Helpers
{
    abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override void SetValue(IDbDataParameter parameter, T? value)
            => parameter.Value = value;
    }

    class DateTimeHandler : SqliteTypeHandler<DateTime>
    {
        public override DateTime Parse(object value)
            => DateTime.Parse((string)value);
    }

    class GuidHandler : SqliteTypeHandler<Guid>
    {
        public override Guid Parse(object value)
            => Guid.Parse((string)value);
    }

    class TimeSpanHandler : SqliteTypeHandler<TimeSpan>
    {
        public override TimeSpan Parse(object value)
            => TimeSpan.Parse((string)value);
    }
}
