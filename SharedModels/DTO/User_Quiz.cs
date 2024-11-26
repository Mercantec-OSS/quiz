namespace SharedModels.DTO;

public class User_QuizDTO
{
    public DateTime? QuizEndDate { get; set; }

    // This is used to flag indicate if the quiz is completed
    public bool Completed { get; set; } = false;

    // This is used to check the results of the quiz's
    public int Results { get; set; } = 0;

    // The ID of the Quiz
    public int QuizID { get; set; }
    
    // The ID of the User
    public int UserID { get; set; }

    public int TimeUsed { get; set; } = 0;
}

public class User_QuizInfoDTO
{
    public DateTime QuizEndDate { get; set; }

    // This is used to flag indicate if the quiz is completed
    public bool Completed { get; set; } = false;

    // This is used to check the results of the quiz's
    public int Results { get; set; } = 0;

    // The ID of the Quiz
    public QuizDTO Quiz { get; set; }

    // The ID of the User
    public UserDTO User { get; set; }

    public int TimeUsed { get; set; } = 0;
}

public class User_QuizUserInfoDTO
{
    public DateTime QuizEndDate { get; set; }

    // This is used to flag indicate if the quiz is completed
    public bool Completed { get; set; } = false;

    // This is used to check the results of the quiz's
    public int Results { get; set; } = 0;

    public int TimeUsed { get; set; }

    public UserDTO User { get; set; }
}

public class User_QuizQuizInfoDTO
{
    public DateTime QuizEndDate { get; set; }

    // This is used to indicate if the quiz is completed
    public bool Completed { get; set; } = false;

    // This is used to check the results of the quiz's
    public int Results { get; set; } = 0;

    public int TimeUsed { get; set; }

    public QuizDTO quiz { get; set; }
}
