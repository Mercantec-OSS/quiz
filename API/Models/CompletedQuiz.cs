using API.Models.API.Models;

namespace API.Models
{
    public class CompletedQuiz
    {
        [Key]
        public int CompletedQuizID { get; set; }
        public bool Completed { get; set; }  // Flag to indicate if the quiz is completed
        public int Results { get; set; }  // Store the result or score of the quiz

        // Foreign key to Quiz
        public int QuizID { get; set; }
        public virtual Quiz Quiz { get; set; }

        // Foreign key to User
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }

    public class CompletedQuizDTO
    {
        public bool Completed { get; set; }  // Flag to indicate if the quiz is completed
        public int Results { get; set; }  // Store the result or score of the quiz

    }
}
