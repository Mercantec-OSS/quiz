namespace SharedModels.DTO;

public class QuizCreateCustomDTO
{
    public int CreatorID { get; set; }

    // These are used to identify the quiz
    public string Title { get; set; } // Fx. Website dev
    public int EducationID { get; set; } // Fx. Buttons
    public int CategoryID { get; set; } // Fx. Website
    public int DifficultyID { get; set; } // Fx. H3
    public int[] questions { get; set; }
}

public class QuizCreateRandomDTO
{
    public int CreatorID { get; set; }

    // These are used to identify the quiz
    public string Title { get; set; } // Fx. Website dev
    public int EducationID { get; set; } // Fx. Buttons
    public int CategoryID { get; set; } // Fx. Website
    public int DifficultyID { get; set; } // Fx. H3
    public QuestionAmount[] questions { get; set;}

}

public class QuizDTO
{
    public int? ID { get; set; }
    public string Creator { get; set; }
    public string Title { get; set; } // Fx. Website dev
    public string Education { get; set; } // Fx. Buttons
    public string Category { get; set; } // Fx. Website
    public string Difficulty { get; set; } // Fx. H3
    public int QestionsAmount { get; set; } = 0;
}

public class QuestionAmount
{
    public int CategoryID { get; set; } // Fx. Website
    public int? UnderCategoryID { get; set; }
    public int DifficultyID { get; set; } // Fx. H3
    public int Amount {  get; set; }
}