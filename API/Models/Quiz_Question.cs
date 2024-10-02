namespace API.Models
{
    public class Quiz_Question
    {
        [Key]
        public int QuizID { get; set; }
        public virtual Quiz Quiz { get; set; }

        public int QuestionID { get; set; }
        public virtual Question Question { get; set; }
    }
}
