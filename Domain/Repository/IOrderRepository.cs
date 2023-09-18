using Domain.Models;

namespace Domain.Repository;

public interface IOrderRepository : IBaseRepository
{
    public Task<IEnumerable<Order>> GetAllOrdersAsync();
    public Task AddAsync(Order order);
}