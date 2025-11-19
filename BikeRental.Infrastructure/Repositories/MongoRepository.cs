using MongoDB.Driver;
using BikeRental.Domain.Models;

namespace BikeRental.Infrastructure.Repositories;

public class MongoRepository<T> : IRepository<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(MongoDbContext context, string collectionName)
    {
        _collection = context.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync()
        => await _collection.Find(_ => true).ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id)
        => await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();

    public async Task CreateAsync(T entity)
        => await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(Guid id, T entity)
        => await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);

    public async Task DeleteAsync(Guid id)
        => await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
}
