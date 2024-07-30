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
        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Difficulty> Difficulty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationship between Quiz and User
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Creator)
                .WithMany() // Assuming User does not have a collection of Quizs
                .HasForeignKey(q => q.CreatorId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: Set the delete behavior
                                                    // Question to Difficulty relationship
         

            // Question to Difficulty relationship
            modelBuilder.Entity<Question>()
                .HasOne(q => q.MainDifficulty)
                .WithMany() // No navigation property in Difficulty
                .HasForeignKey("MainDifficultyId") // Foreign key name
                .IsRequired(false); // Optional relationship if necessary

            // Seed initial data (optional, for testing)
            modelBuilder.Entity<Difficulty>().HasData(
                new Difficulty { Id = 1, DifficultyLevel = "GF2", Points = 100 },
                new Difficulty { Id = 2, DifficultyLevel = "H1", Points = 100 },
                new Difficulty { Id = 3, DifficultyLevel = "H2", Points = 100 },
                new Difficulty { Id = 4, DifficultyLevel = "H3", Points = 100 },
                new Difficulty { Id = 5, DifficultyLevel = "H4", Points = 100 },
                new Difficulty { Id = 6, DifficultyLevel = "H5", Points = 100 }
            );
        }
    }
}