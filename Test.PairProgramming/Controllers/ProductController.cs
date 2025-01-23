using Microsoft.AspNetCore.Mvc;
using Test.PairProgramming.Models;

namespace Test.PairProgramming.Controllers;

public partial class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }


    public IActionResult GetProduct(int id)
    {
        var product = _context.Products.First(p => p.Id == id);
        return Ok(product);
    }

    public IActionResult AddProduct(ProductViewModel model)
    {

        if (!ModelState.IsValid)
        {
            BadRequest(ModelState.ToList().ToLookup(x => x.Key, x => x));
        }


        var product = new Product
        {
            ProductName = model.ProductName,
            CategoryId = model.CategoryId,
            SupplierId = model.SupplierId,
            Price = model.Price
        };

        _context.Products.Add(product);
        _context.SaveChangesAsync();

        var notificationService = new EmailNotificationService();
        notificationService.Send($"Product '{product.ProductName}' has been added.");

        return Ok("Create Product was successful");
    }


    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        var categories = _context.Categories.ToList();
        var suppliers = _context.Suppliers.ToList();

        var productViewModels = new List<ProductViewModel>();

        foreach (var product in products)
        {
            var category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
            var supplier = suppliers.FirstOrDefault(s => s.Id == product.SupplierId);

            productViewModels.Add(new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                CategoryName = category != null ? category.CategoryName : "N/A",
                SupplierName = supplier != null ? supplier.SupplierName : "N/A",
                Price = product.Price
            });
        }

        return View(productViewModels);
    }

}


    

    public class EmailNotificationService 
    {
        public void Send(string message)
        {
            Console.WriteLine($"[Email] {message}");
        }
    }

