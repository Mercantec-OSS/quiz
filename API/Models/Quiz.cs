using API.Models.API.Models;

namespace API.Models
{
    public class Quiz
    {
        [Key]
        public int QuizID { get; set; }
        public List<string> InvitedUsers { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string MainDifficulty { get; set; }
        public List<int>? UserAnswer { get; set; }
        public List<int>? QuizAnswer { get; set; }
        public DateTime QuizDate { get; set; }
        public int Timer { get; set; }

        // Navigation property for questions
        public virtual List<string> Questions { get; set; }
        public int QuestionAmount { get; set; }

        public int UserID { get; set; }
    }

    public class QuizDTO
    {

        public List<string> InvitedUsers { get; set; } // The students that are allowed to participate in the quiz
        public string? Title { get; set; } // Fx. Website dev
        public string? Category { get; set; } // Fx. Website

        // ------------------------------------ //

        public DateTime QuizDate { get; set; }
        public int Timer { get; set; } // Custom timer for more complex functionality

        // ------------------------------------ //

        public string MainDifficulty { get; set; }

        // ------------------------------------ //

        public int UserID { get; set; } // Their ID

        // ------------------------------------ //
        public List<string> Questions { get; set; } // To greate a relation between Quiz and question
        
        public int QuestionAmount { get; set; }
    }

    public class QuizAnswers
    {
        public int Id { get; set; }
        public Quiz QuizId { get; set; }
        public User UserId { get; set; }
        public List<int>? UserAnswer { get; set; }
        public List<int>? QuizAnswer { get; set; }
    }
}