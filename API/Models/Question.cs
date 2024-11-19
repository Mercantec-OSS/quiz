namespace API.Models;

public class Question
{
    [Key]

    // The ID of the Question
    public int ID { get; set; }

    [ForeignKey("Creator")]
    public virtual User CreatorID { get; set; }

    // These are used to identify the question
    public string Title { get; set; }

    [ForeignKey("Category")]
    public virtual Categories category { get; set; }
    
    [ForeignKey("UnderCategory")]
    public virtual UnderCategories underCategory { get; set; }

    [ForeignKey("Difficulty")]
    public virtual Difficulties difficulty { get; set; }

    // These two are used for wrong answers and the correcr answers for the question
    public string[] PossibleAnswers { get; set; }
    public int[] CorrectAnswer { get; set; }
    
    // This is used for a pictures URL 
    public string Picture { get; set; }

    // This is the amount of time there is to complete the quiz in total
    public int Time { get; set; }

    // This is for enabeling/disabeling outdated or for another reason Questions
    public bool QuestionStatus { get; set; }

    public string QuestionType { get; set; }
}
