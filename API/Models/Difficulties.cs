namespace API.Models
{
    public class Difficulties
    {
        [Key]
        public int ID { get; set; }
        public string Difficulty { get; set; }
    }

    public class DifficultiesDTO
    {
        public string Difficulty { get; set; }
    }
}