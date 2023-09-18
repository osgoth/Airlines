using Logic.Models;

namespace Logic.DataProviders.Interfaces;

public interface IFlightsDataProvider
{
    public Task LoadFlights();
    public void SendFlightsInfo(IEnumerable<Flight> flights);
}