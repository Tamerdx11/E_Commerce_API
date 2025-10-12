namespace ECommerce.Persistance.Repositories;

public class Repository<TEntity, TKey>(ApplicationDbContext dbContext)
    : IRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
{
    public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) 
        => await dbContext.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default) 
        => await dbContext.Set<TEntity>().FindAsync(id, cancellationToken);

    public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

    public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);
}
