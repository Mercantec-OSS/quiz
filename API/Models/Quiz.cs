namespace API.Models
{
    public class Quiz
    {
        [Key]

        // The ID of the Quiz
        public int ID { get; set; }
        public int CreatorID { get; set; }
        public virtual User User { get; set; }

        // These are used to identify the quiz
        public string Title { get; set; } // Fx. Website dev

        [ForeignKey("Education")]
        public virtual Educations Educations { get; set; }    

        [ForeignKey("Category")]
        public virtual Categories Categories { get; set; }

        [ForeignKey("Difficulty")]
        public virtual Difficulties Difficulties { get; set; }

        // This is the amount of time there is to complete the quiz in total
        public int Timer { get; set; }

        // This is the amount of question that is in the quiz
        public int QuestionAmount { get; set; }
    }

    public class QuizDTO
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
}