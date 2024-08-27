namespace API.Models
{
    public class Quiz : Common
    {
        public List<string> InvitedUsers { get; set; } // The students that are allowed to participate in the quiz
        public string? Title {  get; set; } // Fx. Website dev
        public string? Category { get; set; } // Fx. Website

        // ------------------------------------ //

        public DateTime QuizDate { get; set; }
        public int AddedTime { get; set; } // Extra time that is able to be added if needed
        public int Timer { get; set; } // Custom timer for more complex functionality

        // ------------------------------------ //

        public string Maindifficulty { get; set; }

        // ------------------------------------ //

        public User Creator { get; set; } // The user that created the quiz
		public int CreatorId { get; set; } // Their ID
        
        // ------------------------------------ //
        
        public List<Question> Questions { get; set; } // To greate a relation between Quiz and question
        public int QuestionId {  get; set; } // Question Id
        public int QuestionAmount { get; set; }
    }

    public class QuizDTO
    {
        public List<string> InvitedUsers { get; set; } // The students that are allowed to participate in the quiz
        public string? Title { get; set; } // Fx. Website dev
        public string? Category { get; set; } // Fx. Website

        // ------------------------------------ //

        public DateTime QuizDate { get; set; }
        public int AddedTime { get; set; } // Custom timer for more complex functionality
        public int Timer { get; set; } // Custom timer for more complex functionality

        // ------------------------------------ //

        public string Maindifficulty { get; set; }

        // ------------------------------------ //

        public int CreatorId { get; set; } // Their ID

        // ------------------------------------ //
        public List<Question> Questions { get; set; } // To greate a relation between Quiz and question
        public int QuestionAmount { get; set; }
    }
}