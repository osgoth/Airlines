using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AirlinesInMemoryContext : DbContext
{
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AirlinesDb");
    }
}