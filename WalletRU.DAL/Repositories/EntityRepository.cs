using System.Data.SqlClient;
using Npgsql;
using WalletRU.DAL.Helpers;

namespace WalletRU.DAL.Repositories;

public abstract class EntityRepository<TEntity> : IRepository<TEntity>, IDisposable, IAsyncDisposable
    where TEntity : class
{
    protected readonly NpgsqlConnection _connection;

    public EntityRepository(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public virtual IEnumerable<TEntity> Get()
    {
        var sqlQuery = $"SELECT * FROM {typeof(TEntity).Name}s";

        return DatabaseHelper.ExecuteSqlQuery<TEntity>(sqlQuery, _connection);
    }

    public virtual TEntity Get(int id)
    {
        var sqlQuery = $"SELECT * FROM {typeof(TEntity).Name}s WHERE id = @id";

        return DatabaseHelper.ExecuteSqlQuery<TEntity>(sqlQuery, _connection, [new SqlParameter("id", id)]).First();
    }

    public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
    {
        return Get().Where(predicate);
    }

    public abstract void Delete(int id);
    public abstract void Update(TEntity entity);
    public abstract void Add(TEntity entity);
}