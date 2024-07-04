using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class Difficulty : Common
    {
        public string DifficultyLevel { get; set; }
        public int Points { get; set; }
    }

    public class DifficultyDTO
    {
        public string DifficultyLevel { get; set; }
        public int Points { get; set; }
    }
}
