using BikeRental.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BikeRental.Infrastructure;

public class MongoDbContext
{
	private readonly IMongoDatabase _database;

	public MongoDbContext(IOptions<MongoDbSettings> options)
	{
		var client = new MongoClient(options.Value.ConnectionString);
		_database = client.GetDatabase(options.Value.DatabaseName);
	}

	public IMongoCollection<T> GetCollection<T>(string name)
	{
		return _database.GetCollection<T>(name);
	}
}
