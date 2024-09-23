using API.Models.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string UnderCategory { get; set; }
        public int[] PossibleAnswers { get; set; } // a list of the placeholder wrong answers
        public int[] CorrectAnswer { get; set; } // The correct answer
        public string Picture { get; set; }
        public int Time { get; set; }
        public bool QuestionStatus { get; set; }

        // Direct difficulty levels inside Question
        public string DifficultyLevel { get; set; } = DifficultyLevels.Unassigned;
        public int Points { get; set; } = 100;

        // Static class to store predefined levels (from the old Difficulty model)
        public static class DifficultyLevels
        {
            public const string Unassigned = "Unassigned"; 
            public const string GF2 = "GF2";
            public const string H1 = "H1";
            public const string H2 = "H2";
            public const string H3 = "H3";
            public const string H4 = "H4";
            public const string H5 = "H5";
        }

        public int UserID { get; set; }
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

        public int UserID { get; set; } // Their ID
    }
}
