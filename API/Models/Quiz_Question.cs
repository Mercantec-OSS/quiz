using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Quiz_Question
    {
        [Key]

        public int ID { get; set; }

        [ForeignKey("Quiz")]
        public virtual Quiz Quizs { get; set; }

        [ForeignKey("Question")]
        public virtual Question Questions { get; set; }
    }

    public class Quiz_QuestionDTO
    {
        public int QuizID { get; set; }
        public int QuestionID { get; set; }
    }
}
