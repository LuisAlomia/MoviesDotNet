using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.EntityFramework.ContextDB
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
