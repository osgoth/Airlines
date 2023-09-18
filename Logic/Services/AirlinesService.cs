using Domain.Repository;
using Logic.DataProviders.Interfaces;
using Logic.Services.Interfaces;
using Flight = Logic.Models.Flight;
using Order = Logic.Models.Order;

namespace Logic.Services;

public class AirlinesService : IAirlinesService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IFlightsDataProvider _flightsDataProvider;
    private readonly IOrdersDataProvider _ordersDataProvider;

    public AirlinesService(IFlightRepository flightRepository, IOrderRepository orderRepository,
        IFlightsDataProvider flightsDataProvider, IOrdersDataProvider ordersDataProvider)
    {
        this._orderRepository = orderRepository;
        this._flightRepository = flightRepository;
        this._flightsDataProvider = flightsDataProvider;
        this._ordersDataProvider = ordersDataProvider;
    }

    public async Task RunService()
    {
        await this._flightsDataProvider.LoadFlights();
        var flights = (await this._flightRepository.GetAllFlightsAsync()).Select(x => new Flight()
        {
            FlightNumber = x.FlightNumber, DayNumber = x.DayNumber, Arrival = x.Arrival, Departure = x.Departure,
            MaxBoxCapacity = x.MaxBoxCapacity
        }).ToList();

        _flightsDataProvider.SendFlightsInfo(flights);

        await this._ordersDataProvider.LoadOrders();
        var orders = (await this._orderRepository.GetAllOrdersAsync()).Select(x => new Order()
            { OrderId = x.OrderId, Destination = x.Destination });

        var destinations = flights.Select(x => x.Arrival).Distinct().ToList();

        var flightToCapacity = flights.ToDictionary(flight => flight.FlightNumber, flight => 0);

        foreach (var order in orders)
        {
            if (!destinations.Contains(order.Destination))
            {
                _ordersDataProvider.SendNoOrderScheduled(order.OrderId);
                continue;
            }

            var currentDay = flights
                .Where(x => flightToCapacity[x.FlightNumber] < x.MaxBoxCapacity
                            && x.Arrival == order.Destination)
                .MinBy(x => x.DayNumber)!.DayNumber;
            var flightByDestination =
                flights.FirstOrDefault(x => x.Arrival == order.Destination
                                            && x.DayNumber == currentDay);
            if (flightByDestination != null)
            {
                order.DayNumber = flightByDestination.DayNumber;
                order.FlightNumber = flightByDestination.FlightNumber;
                order.Departure = flightByDestination.Departure;
                flightToCapacity[flightByDestination.FlightNumber]++;
            }

            _ordersDataProvider.SendScheduledOrder(order);
        }
    }
}