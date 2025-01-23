namespace Test.PairProgramming.Models;

public class Supplier
{
    public int Id { get; set; }
    public string SupplierName { get; set; }

    public ICollection<Product> Products { get; set; }
}


