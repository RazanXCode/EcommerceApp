using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Models;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace EcommerceApp.Controllers
{
    public class ProductController : Controller
    {
        //Objective: We wnat to preform CRUD opreation on the dummy data below 

        // Simulate a database using a list 
        private static List<Product> _products = new List<Product> {

            new Product { Id=1,Name="Laptop" , Price = 999},
            new Product { Id = 2, Name = "Smartphone", Price = 699 },
            new Product { Id = 3, Name = "Tablet", Price = 499 },
            new Product { Id = 4, Name = "Monitor", Price = 299 },
            new Product { Id = 5, Name = "Keyboard", Price = 49 },
            new Product { Id = 6, Name = "Mouse", Price = 25 },
            new Product { Id = 7, Name = "Printer", Price = 199 },
            new Product { Id = 8, Name = "Headphones", Price = 89 },
            new Product { Id = 9, Name = "Speaker", Price = 129 },
            new Product { Id = 10, Name = "External Hard Drive", Price = 149 },
            new Product { Id = 11, Name = "Webcam", Price = 79 },
            new Product { Id = 12, Name = "Router", Price = 99 },
            new Product { Id = 13, Name = "Smartwatch", Price = 249 },
            new Product { Id = 14, Name = "Gaming Console", Price = 399 },
            new Product { Id = 15, Name = "Microphone", Price = 159 },
            new Product { Id = 16, Name = "Graphics Card", Price = 499 },
            new Product { Id = 17, Name = "Power Bank", Price = 59 },
            new Product { Id = 18, Name = "VR Headset", Price = 349 },
            new Product { Id = 19, Name = "Docking Station", Price = 179 },
            new Product { Id = 20, Name = "Projector", Price = 599 }

        };

        //1-READ
        public IActionResult Index()
        {
            return View(_products);
        }

        //2-CREATE ( Display form ) and (Handle Product Creation Submission)
        /*
         * This method handles HTTP GET requests.
           It renders a view where users can input product details.*/
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /*[HttpPost] → This method handles form submissions (POST requests).
         ModelState.IsValid → Checks if the submitted product object meets validation rules defined in the Product model (e.g., required fields, data types, constraints).*/

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = _products.Max(p => p.Id) + 1;
                _products.Add(product);
                return RedirectToAction("Index"); // Redirects to the Index action

            }
            return View(product);
        }

        // 3-EDIT 
        /*HTTP Method: GET
         Purpose: Displays the edit form pre-filled with the existing product details*/

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        /*
         HTTP Method: POST
        Purpose: Handles form submissions for updating an existing product.*/
        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            if (ModelState.IsValid) //Ensures the submitted data is valid based on model validation rules (e.g., required fields, data types).
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == updatedProduct.Id);//Searches _products for the product that matches updatedProduct.Id.
                if (existingProduct == null) return NotFound();
                // Updates the Name and Price properties of the existing product with the new values.
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;

                return RedirectToAction("Index");
            }

            return View(updatedProduct);


        }

        //4-DELETE 
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);

        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _products.Remove(product);
            return RedirectToAction("Index");

        }

    }
}
