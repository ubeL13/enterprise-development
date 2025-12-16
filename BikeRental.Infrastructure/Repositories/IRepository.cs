namespace BikeRental.Infrastructure.Repositories;

/// <summary>
/// Generic repository interface for basic CRUD operations.
/// </summary>
public interface IRepository<T>
{
    /// <summary>
    /// Retrieves all entities of type T.
    /// </summary>
    public Task<List<T>> GetAllAsync();

    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    public Task<T?> GetByIdAsync(string id);

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    public Task CreateAsync(T entity);

    /// <summary>
    /// Updates an existing entity by its identifier.
    /// </summary>
    public Task UpdateAsync(string id, T entity);

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    public Task DeleteAsync(string id);
}
