using API.Models;

namespace API.Models
{
    public class Roles
    {
        [Key]

        public int ID { get; set; }
        public string Role { get; set; }
    }

    public class RolesDTO
    {
        public string Role { get; set; }
    }
}
