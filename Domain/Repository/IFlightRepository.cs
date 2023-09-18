using Domain.Models;

namespace Domain.Repository;

public interface IFlightRepository : IBaseRepository
{
    public Task AddRangeAsync(IEnumerable<Flight> flights);
    public Task<IEnumerable<Flight>> GetAllFlightsAsync();
}