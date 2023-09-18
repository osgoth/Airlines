namespace Domain.Repository;

public interface IBaseRepository
{
    public Task SaveChangesAsync();
}