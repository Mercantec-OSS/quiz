namespace API.Models
{
    public class Difficulty
    {
        public int Id { get; set; }
        public string DifficultyLevel { get; set; }
        public int Points { get; set; } = 100;
        public class Levels
        {
            public const string Unassigned = "Unassigned";
            public const string GF2 = "GF2";
            public const string H1 = "H1";
            public const string H2 = "H2";
            public const string H3 = "H3";
            public const string H4 = "H4";
            public const string H5 = "H5";
        }
    }

    public class DifficultyDTO
    {
        public string DifficultyLevel { get; set; }
        public int Points { get; set; } = 100;
    }
}
