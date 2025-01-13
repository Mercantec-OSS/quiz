namespace API.Models;

public class User_Quiz
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Quiz")]
    public virtual Quiz quiz { get; set; }

    [ForeignKey("User")]
    public virtual User user { get; set; }

    public bool Completed { get; set; }
    
    public double Results { get; set; }

    public DateTime QuizEndDate { get; set; }
    
    public int TimeUsed { get; set; }
}
