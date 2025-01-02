using Microsoft.AspNetCore.Mvc;
using ModelndView.Model;
namespace ModelndView.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Create a list of ProductViewModel
            var productViewModels = new List<ProductViewModel>
            {
                new ProductViewModel()
                {
                    Product = new Product() { Id = 101, Name = "Laptop", Price = 75000 },
                    CategoryName = "Electronics",
                    isInStock = false,
                    Message = "Company: Asus, i7 gen, RAM: 16GB"
                },
                new ProductViewModel()
                {
                    Product = new Product() { Id = 102, Name = "Smartphone", Price = 25000 },
                    CategoryName = "Electronics",
                    isInStock = true,
                    Message = "Company: Samsung, 6GB RAM, 128GB Storage"
                },
                new ProductViewModel()
                {
                    Product = new Product() { Id = 103, Name = "Headphones", Price = 2000 },
                    CategoryName = "Accessories",
                    isInStock = true,
                    Message = "Company: Sony, Noise Cancelling"
                }
            };

            // Return the list to the view
            return View(productViewModels);
        }
        public IActionResult Test(Product p)
        {
            return View(p);
        }
        public IActionResult ProductDeatils(int id)
        {
            Product product = new Product() { Id = 101, Name = "Laptop", Price = 75000 };
            return View(product);
        }
    }


}
