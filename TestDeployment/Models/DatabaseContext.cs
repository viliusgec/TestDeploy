using Microsoft.EntityFrameworkCore;

namespace TestDeployment.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<PlayerItems> PlayerItems { get; set; }
    }
}
