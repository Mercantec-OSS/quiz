namespace API.Models;

public class Quiz
{
    [Key]
    public int ID { get; set; }

    [ForeignKey("Creator")]
    public virtual User creator { get; set; }

    public string Title { get; set; }

    [ForeignKey("Education")]
    public virtual Educations education { get; set; }    

    [ForeignKey("Category")]
    public virtual Categories category { get; set; }

    [ForeignKey("Difficulty")]
    public virtual Difficulties difficulty { get; set; }
}
