using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Quiz_Question
    {
        [Key]

        public int Id { get; set; }

        [ForeignKey("Quiz")]
        public int QuizID { get; set; }
        public virtual Quiz Quiz { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
    }

    public class Quiz_QuestionDTO
    {
        public int QuizID { get; set; }
        public int QuestionID { get; set; }
    }
}
