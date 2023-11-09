using GeoBuyerParser2.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoBuyerParser.DB;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }

    public DbSet<Location> Locations { get; set; } 
}