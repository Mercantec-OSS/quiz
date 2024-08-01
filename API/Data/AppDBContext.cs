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
                new() { DifficultyLevel = Models.Difficulty.Levels.GF2, Id = 1 },
                new() { DifficultyLevel = Models.Difficulty.Levels.H1, Id = 2 },
                new() { DifficultyLevel = Models.Difficulty.Levels.H2, Id = 3 },
                new() { DifficultyLevel = Models.Difficulty.Levels.H3, Id = 4 },
                new() { DifficultyLevel = Models.Difficulty.Levels.H4, Id = 5 },
                new() { DifficultyLevel = Models.Difficulty.Levels.H5, Id = 6 }
            );
        }
    }
}