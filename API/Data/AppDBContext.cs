using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{

    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}