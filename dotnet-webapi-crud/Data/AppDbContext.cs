using Microsoft.EntityFrameworkCore;
using DotnetWebApiCrud.Models;

namespace DotnetWebApiCrud.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
