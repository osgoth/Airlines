namespace Infrastructure.Repository;

public class BaseRepository
{
    protected readonly AirlinesInMemoryContext _dbContext;

    protected BaseRepository(AirlinesInMemoryContext context)
    {
        this._dbContext = context;
    }

    public async Task SaveChangesAsync()
    {
        await this._dbContext.SaveChangesAsync();
    }
}