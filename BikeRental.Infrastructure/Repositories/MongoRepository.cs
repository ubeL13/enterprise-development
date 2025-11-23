using MongoDB.Driver;
using BikeRental.Domain.Models;
using BikeRental.Infrastructure.Settings;

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

    public async Task<T?> GetByIdAsync(string id)
        => await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();

    public async Task CreateAsync(T entity)
        => await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(string id, T entity)
        => await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);

    public async Task DeleteAsync(string id)
        => await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
}
