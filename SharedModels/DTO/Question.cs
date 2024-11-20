namespace SharedModels.Models;

public class QuestionDTO
{
    public int? ID { get; set; }
    public string Creator { get; set; }

    // These are used to identify the question
    public string Title { get; set; }
    public string Category { get; set; }
    public string UnderCategory { get; set; }
    public string Difficulty { get; set; }

    // These two are used for wrong answers and the correcr answers for the question
    public string[] PossibleAnswers { get; set; }
    public int[] CorrectAnswer { get; set; }

    // This is used for a pictures URL 
    public string Picture { get; set; }

    // This is the amount of time there is to complete the quiz in total
    public int Time { get; set; }

    // This is for enabeling/disabeling outdated or for another reason Questions
    public bool QuestionStatus { get; set; }

    public string QuestionType { get; set; } = "Multi";
}

public class QuestionCreateDTO
{
    public int Creator { get; set; }

    // These are used to identify the question
    public string Title { get; set; }
    public int Category { get; set; }
    public int UnderCategory { get; set; }
    public int Difficulty { get; set; }

    // These two are used for wrong answers and the correcr answers for the question
    public string[] PossibleAnswers { get; set; }
    public int[] CorrectAnswer { get; set; }

    // This is used for a pictures URL 
    public string Picture { get; set; }

    // This is the amount of time there is to complete the quiz in total
    public int Time { get; set; }

    // This is for enabeling/disabeling outdated or for another reason Questions
    public bool QuestionStatus { get; set; }

    public string QuestionType { get; set; } = "Multi";
}
