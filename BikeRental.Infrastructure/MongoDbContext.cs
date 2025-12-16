using Microsoft.EntityFrameworkCore;
using BikeRental.Domain.Models;

namespace BikeRental.Infrastructure;

public class MongoDbContext : DbContext
{
    public MongoDbContext(DbContextOptions<MongoDbContext> options)
        : base(options) { }

    public DbSet<Bike> Bikes => Set<Bike>();
    public DbSet<BikeModel> BikeModels => Set<BikeModel>();
    public DbSet<Renter> Renters => Set<Renter>();
    public DbSet<Rental> Rentals => Set<Rental>();
}
