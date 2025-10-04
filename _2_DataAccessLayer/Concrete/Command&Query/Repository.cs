using System.Runtime.CompilerServices;
using _2_DataAccessLayer.Concrete;
using Microsoft.Extensions.Logging;

public class Repository
{
    private readonly ApplicationDbContext _context;
    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public IQueryable<T> Export<T>(ILogger logger, [CallerMemberName] string methodName = "") where T : class
    {
        logger.LogInformation($"{this.GetType().Name}.{methodName}: Exporting entities of type {typeof(T).Name}");
        return _context.Set<T>().AsQueryable();
    }
}