namespace SharedModels.DTO;

public class UnderCategoryCreateDTO
{
    public string UnderCategory { get; set; }
    public int CategoryID { get; set; }
}

public class UnderCategoryGetDTO
{
    public int ID { get; set; }
    public string UnderCategory { get; set; }

}
