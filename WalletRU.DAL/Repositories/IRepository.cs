namespace WalletRU.DAL.Repositories;

public interface IRepository<TEntity>
    where TEntity: class
{
    public TEntity Get(int id);
    public IEnumerable<TEntity> Get();
    public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    public void Add(TEntity entity);
    public void Delete(TEntity entity);
    public void Update(TEntity entity);
}