using API.Models;

public class QuizTimer
{
    public int? Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;

    public bool IsActive => DateTime.Now < EndTime;
}

namespace API.Models
{
    public class Quiz : Common
    {
        public string Title {  get; set; }
        public Question Question { get; set; }
        public int QuestionAmount { get; set; }
        public string InvitedUsers { get; set; }
		public User Creator { get; set; }
		public int CreatorId { get; set; }
        public QuizTimer Timer { get; set; } // Custom timer for more complex functionality
        public Difficulty difficulty { get; set; }
        public string Maindifficulty { get; set; }
    }

    public class QuizDTO
    {
        public string Title { get; set; }
        public int QuestionAmount { get; set; }
        public string InvitedUsers { get; set; }
        public QuizTimer Timer { get; set; } // Custom timer for more complex functionality
        public string Maindifficulty { get; set;}
    }
}