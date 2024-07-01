namespace API.Models
{
    public class Quiz : Common
    {
        public string Title {  get; set; }
        public Question QuestionId { get; set; }
        public string InvitedUsers { get; set; }
		public User Creator { get; set; }
		public int CreatorId { get; set; }
        public Timer Timer { get; set; } // Overall quiz timer
    }
}