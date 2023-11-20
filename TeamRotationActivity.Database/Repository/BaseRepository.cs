using Microsoft.EntityFrameworkCore;
using TeamRotationActivity.Database.Data;
using TeamRotationActivity.Domain.Interfaces;
using TeamRotationActivity.Domain.Interfaces.Repository;

namespace TeamRotationActivity.Database.Repository;

/// <summary>
/// Базовый репозиторий.
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class BaseRepository<TId, TEntity> : IRepository<TId, TEntity>
    where TEntity : class, IEntity<TId>
    where TId : struct
{
    private readonly TeamRotationActivityDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// конструктор класса.
    /// </summary>
    /// <param name="context"></param>
    public BaseRepository(TeamRotationActivityDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> Query => _dbSet;

    /// <inheritdoc />
    public ICollection<TEntity> GetAllAsync()
    {
        return Query.AsNoTracking().ToList();
    }

    /// <inheritdoc />
    public async Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await Query
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return await Query.AsNoTracking().AnyAsync(x => x.Id.Equals(entity.Id), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entityDb = await _dbSet.AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return entityDb.Entity.Id;
    }

    /// <inheritdoc />
    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var updateEntity = _dbSet.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return updateEntity.Entity;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        if (entity == null)
        {
            throw new InvalidOperationException("Entity not exist");
        }

        _dbSet.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}

