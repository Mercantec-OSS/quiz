namespace API.Models
{
    public class Question : Common
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public List<string> PossibleAnswers { get; set; }
        public string CorrectAnswer { get; set; }
        public string Picture { get; set; }
        public string Difficulty { get; set; }
        public User CreatorÍd { get; set; }
    }

    public class QuestionsCategories
    {
        public string Category { get; set; }
    }
}
