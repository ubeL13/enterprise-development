using System;

namespace BikeRental.Domain.Models
{
    public class Rental
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Bike Bike { get; set; }      
        public required Renter Renter { get; set; }    
        public DateTime StartTime { get; set; }         
        public int DurationHours { get; set; }         
        public decimal GetTotalPrice() => Bike.Model.HourlyRate * DurationHours;
    }
}
