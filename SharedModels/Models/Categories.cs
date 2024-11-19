﻿namespace SharedModels.Models;

public class CategoriesDTO
{
    public int ID { get; set; }
    public string Category { get; set; }

    public int EducationID { get; set; }
    
}

public class CategoryCreateDTO
{
    public string Category { get; set; }
    public int EducationID { get; set; }
}
