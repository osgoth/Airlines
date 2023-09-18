using Domain.Models;
using Domain.Repository;
using Logic.DataProviders.Interfaces;
using Newtonsoft.Json;

namespace ConsoleApp.DataProviders.ConsoleInputSimulation;

public class OrdersConsoleDataMockProvider : IOrdersDataProvider
{
    private readonly IOrderRepository _orderRepository;

    public OrdersConsoleDataMockProvider(IOrderRepository orderRepository)
    {
        this._orderRepository = orderRepository;
    }

    public async Task LoadOrders()
    {
        Console.WriteLine("Loading the list of orders.");
        Console.Write("Input full path to json: ");
        var jsonPath = Console.ReadLine();
        using (StreamReader sr = new StreamReader(jsonPath))
        {
            var json = await sr.ReadToEndAsync();
            var deserializedJson = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
            foreach (var v in deserializedJson)
            {
                await this._orderRepository.AddAsync(new Order()
                    { OrderId = v.Key, Destination = v.Value["destination"] });
            }

            await this._orderRepository.SaveChangesAsync();
        }

        Console.WriteLine("The list of orders has been loaded to the database.");
    }

    public void SendNoOrderScheduled(string orderNumber)
    {
        Console.WriteLine($"order: {orderNumber}, flightNumber: not scheduled");
    }

    public void SendScheduledOrder(Logic.Models.Order order)
    {
        Console.WriteLine(
            $"order: {order.OrderId}, flightNumber: {order.FlightNumber}, departure: {order.Departure}, arrival: {order.Destination}, day: {order.DayNumber}");
    }
}