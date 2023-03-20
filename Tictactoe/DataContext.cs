using Microsoft.EntityFrameworkCore;
using Tictactoe.Models;

namespace Tictactoe
{
    public class DataContext : DbContext
    {
        public DbSet<Model> Models { get; set; }
        public DataContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=map.db");
        }
    }
}
