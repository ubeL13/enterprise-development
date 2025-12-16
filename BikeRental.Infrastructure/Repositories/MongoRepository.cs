using Microsoft.EntityFrameworkCore;

namespace BikeRental.Infrastructure.Repositories;

/// <summary>
/// Generic MongoDB repository implementing basic CRUD operations.
/// </summary>
public class MongoRepository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _context;

    /// <summary>
    /// Initializes a new instance of the repository with a MongoDB collection.
    /// </summary>
    public MongoRepository(MongoDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    /// <summary>
    /// Retrieves all documents from the collection.
    /// </summary>
    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

    /// <summary>
    /// Retrieves a document by its identifier.
    /// </summary>
    public async Task<T?> GetByIdAsync(string id)
    {
        // Предполагается, что у всех сущностей есть строковое поле Id
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Inserts a new document into the collection.
    /// </summary>
    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Replaces an existing document identified by the given ID.
    /// </summary>
    public async Task UpdateAsync(string id, T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a document by its identifier.
    /// </summary>
    public async Task DeleteAsync(string id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
