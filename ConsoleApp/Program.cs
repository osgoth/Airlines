using ConsoleApp.DataProviders.ConsoleInputSimulation;
using Domain.Repository;
using Infrastructure;
using Infrastructure.Repository.InMemoryRepository;
using Logic.DataProviders.Interfaces;
using Logic.Services;
using Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IOrderRepository, OrderRepository>()
            .AddSingleton<IFlightRepository, FlightRepository>()
            .AddSingleton<IOrderFlightService, OrderFlightService>()
            .AddSingleton<IFlightsDataProvider, FlightsConsoleMockDataProvider>()
            .AddSingleton<IOrdersDataProvider, OrdersConsoleDataMockProvider>()
            .AddDbContext<AirlinesInMemoryContext>()
            .BuildServiceProvider();

        var airlinesService = serviceProvider.GetService<IOrderFlightService>();
        await airlinesService.AssignFlightsToOrders();
    }
}