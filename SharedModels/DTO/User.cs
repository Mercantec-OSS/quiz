namespace SharedModels.DTO;

public class UserDTO
{
    public int ID { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public string role { get; set; }
    public string? token { get; set; }
}

public class LoginRequest
{
    public string username { get; set; }
    public string password { get; set; }
}

public class SignUpRequest
{
    public string username { get; set; }
    public string password { get; set; }
}

public class UpdatePassword
{
    public int ID { get; set; }
    public string password { get; set; }
}

public class UpdateUserDTO
{
    public int ID { get; set; }
    public string email { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public int role { get; set; }
}
