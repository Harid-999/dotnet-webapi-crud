namespace DotnetWebApiCrud.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class ModifyProduct
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
