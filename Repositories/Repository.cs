using GeoBuyerParser.DB;
using GeoBuyerParser2.Models;

namespace GeoBuyerParser2.Repositories;

public class Repository
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void InsertLocations(IEnumerable<Location> locations)
    {
        _dbContext.Locations.AddRange(locations);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Location> GetLocations()
    {
        return _dbContext.Locations;
    }
}
