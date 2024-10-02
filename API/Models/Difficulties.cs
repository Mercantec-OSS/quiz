namespace API.Models
{
    public class Difficulties
    {
        [Key]
        public int ID { get; set; }
        public int Difficulty { get; set; }

        //we need to have a difficultyName
        //public string difficultyName { get; set; }
    }
}