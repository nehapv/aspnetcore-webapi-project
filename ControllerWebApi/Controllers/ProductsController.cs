using Microsoft.AspNetCore.Mvc;
using MyFirstWebAPIProject.Models;
using MyFirstWebAPIProject.DTOs;

namespace MyFirstWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Hardcoded Categories
        private static List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Furniture" },
        };

        // Hardcoded Products
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1000.00m, CategoryId = 1 },
            new Product { Id = 2, Name = "Desktop", Price = 2000.00m, CategoryId = 1 },
            new Product { Id = 3, Name = "Chair", Price = 150.00m, CategoryId = 2 },
        };

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var productDTOs = _products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryName = _categories.FirstOrDefault(c => c.Id == p.CategoryId)?.Name ?? "Unknown"
            }).ToList();

            return Ok(productDTOs);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = _categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name ?? "Unknown"
            };

            return Ok(productDTO);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<ProductDTO> PostProduct([FromBody] ProductCreateDTO createDto)
        {
            var newProduct = new Product
            {
                Id = _products.Max(p => p.Id) + 1,
                Name = createDto.Name,
                Price = createDto.Price,
                CategoryId = createDto.CategoryId
            };

            _products.Add(newProduct);

            var productDTO = new ProductDTO
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Price = newProduct.Price,
                CategoryName = _categories.FirstOrDefault(c => c.Id == newProduct.CategoryId)?.Name ?? "Unknown"
            };

            return CreatedAtAction(nameof(GetProduct), new { id = productDTO.Id }, productDTO); //201, Response Body, Location
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest(new { Message = "ID mismatch between route and body." });
            }

            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            // Update product
            existingProduct.Name = updateDto.Name;
            existingProduct.Price = updateDto.Price;
            existingProduct.CategoryId = updateDto.CategoryId;

            return NoContent();  // 204 sucess ,No Content 
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            _products.Remove(product);
            return NoContent(); // 204 sucess ,No Content
        }
    }
}
