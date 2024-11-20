namespace SharedModels.Models;

public class UnderCategoriesCreateDTO
{
    public string UnderCategory { get; set; }
    public int CategoryID { get; set; }
}

public class UnderCategoriesGetDTO
{
    public int ID { get; set; }
    public string UnderCategory { get; set; }

}
