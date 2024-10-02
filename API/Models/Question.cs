using API.Models.API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Question
    {
        [Key]

        // The ID of the Question
        public int ID { get; set; }

        public int CreatorID { get; set; }
        public virtual User User { get; set; }

        // These are used to identify the question
        public string Title { get; set; }
        public int CategoryID { get; set; } // Fx. Website
        public virtual Categories Categories { get; set; }
        public int UnderCategoryID { get; set; }
        public virtual UnderCategories UnderCategories { get; set; }
        public int DifficultyID { get; set; } // Fx. H3
        public virtual Difficulties Difficulties { get; set; }

        // These two are used for wrong answers and the correcr answers for the question
        public string[] PossibleAnswers { get; set; }
        public int[] CorrectAnswer { get; set; }
        
        // This is used for a pictures URL 
        public string Picture { get; set; }

        // This is the amount of time there is to complete the quiz in total
        public int Time { get; set; }

        // This is for enabeling/disabeling outdated or for another reason Questions
        public bool QuestionStatus { get; set; }
    }


    public class QuestionDTO
    {
        public virtual User User { get; set; }

        // These are used to identify the question
        public string Title { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual UnderCategories UnderCategories { get; set; }
        public virtual Difficulties Difficulties { get; set; }

        // These two are used for wrong answers and the correcr answers for the question
        public string[] PossibleAnswers { get; set; }
        public int[] CorrectAnswer { get; set; }

        // This is used for a pictures URL 
        public string Picture { get; set; }

        // This is the amount of time there is to complete the quiz in total
        public int Time { get; set; }

        // This is for enabeling/disabeling outdated or for another reason Questions
        public bool QuestionStatus { get; set; }
    }
}
