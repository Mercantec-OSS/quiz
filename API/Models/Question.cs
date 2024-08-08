namespace API.Models
{
    public class Question : Common
    {
        public string? Title { get; set; } // Fx. Programming
        public string? Category { get; set; } // Fx. Website
        public string? UnderCategory{ get; set; } // Fx. Buttons

        // ------------------------------------ //

        public List<string> PossibleAnswers { get; set; } // a list of the placeholder wrong answers
        public string CorrectAnswer { get; set; } // The correct answer
        public string? Picture { get; set; } // URL for the picture
        public int Time { get; set; } // The time a user gets to answer the question 
        public bool QuestionStatus { get; set; } // To make a question active or inactive

        // ------------------------------------ //

        public string DifficultyLevel { get; set; }
        public Difficulty MainDifficulty { get; set; } // To create a relation between questions to difficulty

        // ------------------------------------ //

        public User Creator { get; set; } // The user that created the quiz
        public int CreatorId { get; set; } // Their ID
    }

    public class QuestionDTO
    {
        public string? Title { get; set; } // Fx. Website dev
        public string? Category { get; set; } // Fx. Programming
        public string? UnderCategory { get; set; } // Fx. Buttons

        // ------------------------------------ //

        public List<string> PossibleAnswers { get; set; } // a list of the placeholder wrong answers
        public string CorrectAnswer { get; set; } // The correct answer
        public string? Picture { get; set; } // URL for the picture
        public int Time { get; set; } // The time a user gets to answer the question 

        // ------------------------------------ //

        public string DifficultyLevel { get; set; }
        public bool QuestionStatus { get; set; } // To make a question active or inactive

        // ------------------------------------ //

        public int CreatorId { get; set; } // Their ID
    }
}
