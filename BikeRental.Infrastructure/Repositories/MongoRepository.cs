using MongoDB.Driver;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Settings;

namespace BikeRental.Infrastructure.Repositories;

/// <summary>
/// Generic MongoDB repository implementing basic CRUD operations.
/// </summary>
public class MongoRepository<T> : IRepository<T>
{
    private readonly IMongoCollection<T> _collection;

    /// <summary>
    /// Initializes a new instance of the repository with a MongoDB collection.
    /// </summary>
    public MongoRepository(MongoDbContext context, string collectionName)
    {
        _collection = context.GetCollection<T>(collectionName);
    }

    /// <summary>
    /// Retrieves all documents from the collection.
    /// </summary>
    public async Task<List<T>> GetAllAsync()
        => await _collection.Find(_ => true).ToListAsync();

    /// <summary>
    /// Retrieves a document by its identifier.
    /// </summary>
    public async Task<T?> GetByIdAsync(string id)
        => await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();

    /// <summary>
    /// Inserts a new document into the collection.
    /// </summary>
    public async Task CreateAsync(T entity)
        => await _collection.InsertOneAsync(entity);

    /// <summary>
    /// Replaces an existing document identified by the given ID.
    /// </summary>
    public async Task UpdateAsync(string id, T entity)
        => await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);

    /// <summary>
    /// Deletes a document by its identifier.
    /// </summary>
    public async Task DeleteAsync(string id)
        => await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
}
