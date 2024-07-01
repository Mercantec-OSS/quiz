namespace API.Models
{
    public class Question : Common
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public string CorrectAnswer { get; set; }
        public string Picture { get; set; }
        public Difficulty Difficulty { get; set; }
        public User Creator { get; set; }
        public int CreatorId { get; set; }
    }

    public class QuestionDTO
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public string CorrectAnswer { get; set; }
        public string? Picture { get; set; }
        public string Difficulty { get; set; }
        public int CreatorId { get; set; }
    }

    public class QuestionsCategories
    {
        public string Category { get; set; }
    }
}
