using Domain.Repository;
using Logic.DataProviders.Interfaces;
using Logic.Models;

namespace ConsoleApp.DataProviders.ConsoleInputSimulation;

public class FlightsConsoleMockDataProvider : IFlightsDataProvider
{
    private readonly IFlightRepository _flightRepository;

    public FlightsConsoleMockDataProvider(IFlightRepository flightRepository)
    {
        this._flightRepository = flightRepository;
    }

    public async Task LoadFlights()
    {
        Console.WriteLine("Loading the list of flights.");
        var flights = new[]
        {
            new Flight() { FlightNumber = 1, DayNumber = 1, Arrival = "YYZ", Departure = "YUL", MaxBoxCapacity = 20 },
            new Flight() { FlightNumber = 2, DayNumber = 1, Arrival = "YYC", Departure = "YUL", MaxBoxCapacity = 20 },
            new Flight() { FlightNumber = 3, DayNumber = 1, Arrival = "YVR", Departure = "YUL", MaxBoxCapacity = 20 },
            new Flight() { FlightNumber = 4, DayNumber = 2, Arrival = "YYZ", Departure = "YUL", MaxBoxCapacity = 20 },
            new Flight() { FlightNumber = 5, DayNumber = 2, Arrival = "YYC", Departure = "YUL", MaxBoxCapacity = 20 },
            new Flight() { FlightNumber = 6, DayNumber = 2, Arrival = "YVR", Departure = "YUL", MaxBoxCapacity = 20 },
        };

        await this._flightRepository.AddRangeAsync(
            flights.Select(
                x => new Domain.Models.Flight()
                {
                    FlightNumber = x.FlightNumber, DayNumber = x.DayNumber, Arrival = x.Arrival,
                    Departure = x.Departure, MaxBoxCapacity = x.MaxBoxCapacity
                }
            )
        );
        await this._flightRepository.SaveChangesAsync();
        Console.WriteLine("List of flights has been loaded to the database.");
    }

    public void SendFlightsInfo(IEnumerable<Flight> flights)
    {
        foreach (var f in flights)
        {
            Console.WriteLine(
                $"Flight: {f.FlightNumber}, departure: {f.Departure}, arrival: {f.Arrival}, day: {f.DayNumber}");
        }
    }
}