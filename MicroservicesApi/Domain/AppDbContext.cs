using Microsoft.EntityFrameworkCore;
using MicroservicesApi;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<News> Newss { get; set; }
}
