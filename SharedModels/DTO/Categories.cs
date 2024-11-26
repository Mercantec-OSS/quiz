namespace SharedModels.DTO;

public class CategoryDTO
{
    public int ID { get; set; }
    public string Category { get; set; }

    public string Education { get; set; }
    
}

public class CategoryCreateDTO
{
    public string Category { get; set; }
    public int EducationID { get; set; }
}
