namespace API.Models
{
    public class Question : Common
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string UnderCategory { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public string CorrectAnswer { get; set; }
        public string Picture { get; set; }
        public Difficulty MainDifficulty { get; set; }
        public string DifficultyLevel { get; set; }
        public User Creator { get; set; }
        public int CreatorId { get; set; }
        public DateTime Timer { get; set; } // Custom timer for more complex functionality
    }

    public class QuestionDTO
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string UnderCategory { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public string CorrectAnswer { get; set; }
        public string? Picture { get; set; }
        public string DifficultyLevel { get; set; }
        public int CreatorId { get; set; }
        public DateTime Timer { get; set; } // Custom timer for more complex functionality
    }
}
