namespace Test.PairProgramming.Models;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
    public decimal Price { get; set; }

    public Category Category { get; set; }
    public Supplier Supplier { get; set; }
}


