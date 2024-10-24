using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Quiz
    {
        [Key]

        // The ID of the Quiz
        public int ID { get; set; }

        [ForeignKey("Creator")]
        public virtual User creator { get; set; }

        // These are used to identify the quiz
        public string Title { get; set; } // Fx. Website dev

        [ForeignKey("Education")]
        public virtual Educations education { get; set; }    

        [ForeignKey("Category")]
        public virtual Categories category { get; set; }

        [ForeignKey("Difficulty")]
        public virtual Difficulties difficulty { get; set; }

        // This is the amount of time there is to complete the quiz in total
        public int Timer { get; set; }

        // This is the amount of question that is in the quiz
        public int QuestionAmount { get; set; }
    }

    public class QuizCreateDTO
    {
        public int CreatorID { get; set; }

        // These are used to identify the quiz
        public string Title { get; set; } // Fx. Website dev
        public int EducationID { get; set; } // Fx. Buttons
        public int CategoryID { get; set; } // Fx. Website
        public int DifficultyID { get; set; } // Fx. H3

        // This is the amount of time there is to complete the quiz in total
        public int Timer { get; set; }

        // This is the amount of question that is in the quiz
        public int QuestionAmount { get; set; }
    }

    public class QuizDTO
    {
        public int? ID { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; } // Fx. Website dev
        public string Education { get; set; } // Fx. Buttons
        public string Category { get; set; } // Fx. Website
        public string Difficulty { get; set; } // Fx. H3

        // This is the amount of time there is to complete the quiz in total
        public int Timer { get; set; }

        // This is the amount of question that is in the quiz
        public int QuestionAmount { get; set; }
    }
}