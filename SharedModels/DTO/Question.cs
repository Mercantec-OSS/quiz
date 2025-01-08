namespace SharedModels.DTO;

public class QuestionDTO
{
    public int? ID { get; set; }
    public string Creator { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string UnderCategory { get; set; }
    public string Difficulty { get; set; }
    public string[] PossibleAnswers { get; set; }
    public int[] CorrectAnswer { get; set; }

    // This is used for a pictures Path
    public string Picture { get; set; }

    public int Time { get; set; }
    // This is for enabeling/disabeling
    // fx. outdated Questions could be disabled so they cant be chosen for new quizzes
    public bool QuestionStatus { get; set; }

    public string QuestionType { get; set; } = "Multi";
}

public class QuestionCreateDTO
{
    public int Creator { get; set; }

    public string Title { get; set; }
    public int Category { get; set; }
    public int UnderCategory { get; set; }
    public int Difficulty { get; set; }

    public string[] PossibleAnswers { get; set; }
    public int[] CorrectAnswer { get; set; }

    // This is used for a pictures Path
    public string Picture { get; set; }

    public int Time { get; set; }

    // This is for enabeling/disabeling
    // fx. outdated Questions could be disabled so they cant be chosen for new quizzes
    public bool QuestionStatus { get; set; }

    public string QuestionType { get; set; } = "Multi";
}
