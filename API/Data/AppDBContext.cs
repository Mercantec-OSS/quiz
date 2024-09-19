using API.Models.API.Models;

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
        public DbSet<CompletedQuiz> CompletedQuizs { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Question to Quiz (required)
        //    modelBuilder.Entity<Question>()
        //        .HasOne(q => q.Quiz)
        //        .WithMany(qz => qz.Questions)
        //        .HasForeignKey(q => q.QuizID)
        //        .IsRequired(true);

        //    // Quiz to CompletedQuiz (one-to-many)
        //    modelBuilder.Entity<CompletedQuiz>()
        //        .HasOne(cq => cq.Quiz)
        //        .WithMany(q => q.CompletedQuizzes)
        //        .HasForeignKey(cq => cq.QuizID)
        //        .IsRequired(true);

        //    // User to CompletedQuiz (one-to-many)
        //    modelBuilder.Entity<CompletedQuiz>()
        //        .HasOne(cq => cq.User)
        //        .WithMany(u => u.CompletedQuizzes)
        //        .HasForeignKey(cq => cq.UserID)
        //        .IsRequired(true);

        //    // If you need to ensure the DifficultyLevel property only takes certain values, you can configure it as follows:
        //    modelBuilder.Entity<Question>()
        //        .Property(q => q.DifficultyLevel)
        //        .HasConversion<string>()
        //        .HasMaxLength(50);
        //}
    }
}