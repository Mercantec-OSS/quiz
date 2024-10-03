using API.Models;

namespace API.Models
{
    public class User_Quiz
    {
        [Key]

        // The ID of CompletedQuiz
        public int ID { get; set; }

        // Foreign key to Quiz
        public int QuizID { get; set; }
        public virtual Quiz Quiz { get; set; }

        // Foreign key to User
        public int UserID { get; set; }
        public virtual User User { get; set; }

        // This is used to flag indicate if the quiz is completed
        public bool Completed { get; set; }
        
        // This is used to check the results of the quiz's
        public int Results { get; set; }

        // This is the date and time that the quiz is available
        public DateTime QuizEndDate { get; set; }
    }

    public class User_QuizDTO
    {
        // This is used to flag indicate if the quiz is completed
        public bool Completed { get; set; }

        // This is used to check the results of the quiz's
        public int Results { get; set; }

        // The ID of the Quiz
        public int QuizID { get; set; }
        
        // The ID of the User
        public int UserID { get; set; }
    }
}
