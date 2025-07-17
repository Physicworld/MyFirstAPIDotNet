using AutoMapper;

namespace testwithnet8.Data;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public int Cost { get; set; }
    public int Price { get; set; }
}

public class ProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    
    public CategoryDto CategoryDto { get; set; }
};

public class UpdateProductDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public int Cost { get; set; }
    public int Price { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public List<Product> Products { get; set; }
}

public class CategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
};

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Category, CategoryDto>();
    }
};