namespace API.Models;

public class User_Quiz
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Quiz")]
    public virtual Quiz quiz { get; set; }

    [ForeignKey("User")]
    public virtual User user { get; set; }

    // This is used to flag indicate if the quiz is completed
    public bool Completed { get; set; }
    
    // This is used to check the results of the quiz's
    public int Results { get; set; }

    // This is the date and time that the quiz is available
    public DateTime QuizEndDate { get; set; }
    
    public int TimeUsed { get; set; }
}
