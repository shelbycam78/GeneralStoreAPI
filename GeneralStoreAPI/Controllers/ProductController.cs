using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        
        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct([FromBody] Product product)
        {
            if (product is null)
            {
                return BadRequest("Your request body can't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            int changecount = await _context.SaveChangesAsync();

            if (changecount < 1)
            {
                return InternalServerError();
            }
            return Ok("Product has been created.");
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetProductBySku([FromUri] string SKU)
        {
            Product product = await _context.Products.FindAsync(SKU);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);     
        }
    }
}
