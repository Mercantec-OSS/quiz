namespace API.Models;

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
}
