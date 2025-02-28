using CalculatorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Calculation> Calculations { get; set; }
    }
}