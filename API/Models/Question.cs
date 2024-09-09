using API.Models.API.Models;

namespace API.Models
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }

        // Foreign key to Quiz
        public int QuizID { get; set; }
        public virtual Quiz Quiz { get; set; }

        public string Title { get; set; }
        public string Category { get; set; }
        public string UnderCategory { get; set; }
        public int[] PossibleAnswers { get; set; }
        public int[] CorrectAnswer { get; set; }
        public string Picture { get; set; }
        public int Time { get; set; }
        public bool QuestionStatus { get; set; }
        public string DifficultyLevel { get; set; }
        public int MainDifficultyId { get; set; } // Foreign key

        // Navigation property to relate to Difficulty
        public virtual Difficulty MainDifficulty { get; set; }

        // Foreign key to the creator of the question
        public int CreatorID { get; set; }

        // Navigation property to relate to Difficulty
        public virtual Difficulty Difficulty { get; set; }
    }


    public class QuestionDTO
    {
        public string? Title { get; set; } // Fx. Website dev
        public string? Category { get; set; } // Fx. Programming
        public string? UnderCategory { get; set; } // Fx. Buttons

        // ------------------------------------ //

        public int[] PossibleAnswers { get; set; } // a list of the placeholder wrong answers
        public int[] CorrectAnswer { get; set; } // The correct answer
        public string? Picture { get; set; } // URL for the picture
        public int Time { get; set; } // The time a user gets to answer the question 

        // ------------------------------------ //

        public string DifficultyLevel { get; set; }
        public bool QuestionStatus { get; set; } // To make a question active or inactive

        // ------------------------------------ //

        public int CreatorId { get; set; } // Their ID
    }
}
