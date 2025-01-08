namespace API.Models;

public class Question
{
    [Key]

    public int ID { get; set; }

    [ForeignKey("Creator")]
    public virtual User CreatorID { get; set; }

    public string Title { get; set; }

    [ForeignKey("Category")]
    public virtual Categories category { get; set; }
    
    [ForeignKey("UnderCategory")]
    public virtual UnderCategories underCategory { get; set; }

    [ForeignKey("Difficulty")]
    public virtual Difficulties difficulty { get; set; }

    public string[] PossibleAnswers { get; set; }
    public int[] CorrectAnswer { get; set; }
    
    public string Picture { get; set; }

    public int Time { get; set; }

    public bool QuestionStatus { get; set; }

    public string QuestionType { get; set; }
}
