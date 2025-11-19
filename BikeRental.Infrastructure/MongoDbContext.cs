using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BikeRental.Infrastructure.Settings;

namespace BikeRental.Infrastructure;

public class MongoDbContext
{
	private readonly IMongoDatabase _db;

	public MongoDbContext(IOptions<MongoDbSettings> options)
	{
		var client = new MongoClient(options.Value.ConnectionString);
		_db = client.GetDatabase(options.Value.DatabaseName);
	}

	public IMongoCollection<T> GetCollection<T>(string name)
		=> _db.GetCollection<T>(name);
}
