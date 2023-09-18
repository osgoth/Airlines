using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.InMemoryRepository;

public class FlightRepository : BaseRepository, IFlightRepository
{
    public FlightRepository(AirlinesInMemoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
    {
        return await _dbContext.Flights.ToArrayAsync();
    }

    public async Task AddRangeAsync(IEnumerable<Flight> flights)
    {
        await _dbContext.Flights.AddRangeAsync(flights);
        await _dbContext.SaveChangesAsync();
    }
}