namespace API.Models;

public class Quiz_Question
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Quiz")]
    public virtual Quiz quiz { get; set; }

    [ForeignKey("Question")]
    public virtual Question question { get; set; }

}
