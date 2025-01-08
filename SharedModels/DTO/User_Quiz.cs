namespace SharedModels.DTO;

public class User_QuizDTO
{
    public DateTime? QuizEndDate { get; set; } = DateTime.UtcNow.AddDays(1);
    public bool Completed { get; set; } = false;

    // Needs to be a percent
    public double Results { get; set; } = 0;

    public int QuizID { get; set; }
    
    public int UserID { get; set; }

    public int TimeUsed { get; set; } = 0;
}

public class CreateMultiUsers_QuizDTO
{
    public DateTime? QuizEndDate { get; set; } = DateTime.UtcNow.AddDays(1);

    public bool Completed { get; set; } = false;

    // Needs to be a percent
    public double Results { get; set; } = 0;

    public int QuizID { get; set; }

    public int[] UserID { get; set; }

    public int TimeUsed { get; set; } = 0;
}

public class User_QuizInfoDTO
{
    public DateTime QuizEndDate { get; set; }

    public bool Completed { get; set; } = false;

    // Needs to be a percent
    public double Results { get; set; } = 0;

    public QuizDTO Quiz { get; set; }

    public UserDTO User { get; set; }

    public int TimeUsed { get; set; } = 0;
}

public class User_QuizUserInfoDTO
{
    public DateTime QuizEndDate { get; set; }

    public bool Completed { get; set; } = false;

    // Needs to be a percent
    public double Results { get; set; } = 0;

    public int TimeUsed { get; set; }

    public UserDTO User { get; set; }
}

public class User_QuizQuizInfoDTO
{
    public DateTime QuizEndDate { get; set; }

    public bool Completed { get; set; } = false;

    // Needs to be a percent
    public double Results { get; set; } = 0;

    public int TimeUsed { get; set; }

    public QuizDTO quiz { get; set; }
}
