using API.Models.API.Models;
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
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Difficulties> Difficulties { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UnderCategories> UnderCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Quiz> User_Quiz { get; set; }

public DbSet<API.Models.Quiz_Question> Quiz_Question { get; set; } = default!;
    }
}