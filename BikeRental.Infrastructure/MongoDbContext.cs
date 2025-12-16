using Microsoft.EntityFrameworkCore;
using BikeRental.Domain.Models;

namespace BikeRental.Infrastructure;

public class MongoDbContext(DbContextOptions<MongoDbContext> options) : DbContext(options)
{
    public DbSet<Bike> Bikes => Set<Bike>();
    public DbSet<BikeModel> BikeModels => Set<BikeModel>();
    public DbSet<Renter> Renters => Set<Renter>();
    public DbSet<Rental> Rentals => Set<Rental>();
}
