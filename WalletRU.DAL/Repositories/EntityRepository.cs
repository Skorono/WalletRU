using System.Data;
using System.Data.SqlClient;
using WalletRU.DAL.Helpers;

namespace WalletRU.DAL.Repositories;

public abstract class EntityRepository<TEntity>: IRepository<TEntity>
    where TEntity: class
{
    private readonly string _connectionString;

    public EntityRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public virtual IEnumerable<TEntity> Get()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string sqlQuery = $"SELECT * FROM {typeof(TEntity).Name}";

        return DatabaseHelper.ExecuteSqlQuery<TEntity>(sqlQuery, connection);
    }

    public virtual TEntity Get(int id)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        string sqlQuery = $"SELECT * FROM {typeof(TEntity).Name} WHERE id = @id";

        return DatabaseHelper.ExecuteSqlQuery<TEntity>(sqlQuery, connection, [new SqlParameter("id", id)]).First();
    }

    public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
    {
        return Get().Where(predicate);
    }

    public virtual void Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public virtual void Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public virtual void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
}