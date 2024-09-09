using API.Models.API.Models;

namespace API.Models
{
    public class Quiz
    {
        [Key]
        public int QuizID { get; set; }

        // Foreign key to User
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public List<string> InvitedUsers { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int[] UserAnswer { get; set; }
        public DateTime QuizDate { get; set; }
        public int Timer { get; set; }
        public string MainDifficulty { get; set; }

        public User Creator { get; set; }
        public int CreatorID { get; set; }

        // Navigation property for questions
        public virtual ICollection<Question> Questions { get; set; }
        public int[] QuestionAmount { get; set; }
    }

    public class QuizDTO
    {
        public List<string> InvitedUsers { get; set; } // The students that are allowed to participate in the quiz
        public string? Title { get; set; } // Fx. Website dev
        public string? Category { get; set; } // Fx. Website

        // ------------------------------------ //

        public string UserAnswer { get; set; }

        // ------------------------------------ //

        public DateTime QuizDate { get; set; }
        public int Timer { get; set; } // Custom timer for more complex functionality

        // ------------------------------------ //

        public string Maindifficulty { get; set; }

        // ------------------------------------ //

        public int CreatorId { get; set; } // Their ID

        // ------------------------------------ //
        public List<Question> Questions { get; set; } // To greate a relation between Quiz and question
        public int[] QuestionAmount { get; set; }
    }
}