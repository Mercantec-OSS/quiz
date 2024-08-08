namespace API.Models
{
    public class Difficulty
    {
        public int Id { get; set; }
        public Levels DifficultyLevel { get; set; } = Levels.Unassigned;
        public int Points { get; set; } = 100;

        public enum Levels // This is another type of array
        {
            Unassigned,
            GF2,
            H1,
            H2,
            H3,
            H4,
            H5,
        }
    }

    public class DifficultyDTO
    {
        public string DifficultyLevel { get; set; } = Difficulty.Levels.Unassigned.ToString();
        public int Points { get; set; } = 100;
    }
}
