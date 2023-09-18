using Logic.Models;

namespace Logic.DataProviders.Interfaces;

public interface IOrdersDataProvider
{
    public Task LoadOrders();
    public void SendNoOrderScheduled(string orderNumber);
    public void SendScheduledOrder(Order order);
}