using Domain.Models;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.InMemoryRepository;

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(AirlinesInMemoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _dbContext.Orders.ToArrayAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await SaveChangesAsync();
    }
}