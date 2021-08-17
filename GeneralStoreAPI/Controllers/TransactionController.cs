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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        
        [HttpPost]
        public async Task<IHttpActionResult> CreateTransaction([FromBody] Transaction transaction)
        {

            Product product = await _context.Products.FindAsync(transaction.ProductSKU);
   
            if (product.IsInStock is false)
            {
                return BadRequest("Product is not in stock");
            }
            if (product.NumberInInventory < 0)
            {
                return BadRequest("Not enough product in Inventory");
            }
            
            _context.Products.Remove(product);

            int changecount = await _context.SaveChangesAsync();
            if (changecount < 1)
            {
                return InternalServerError();
            }

            return Ok("product was removed from inventory");
        }



        [HttpGet]
        public async Task<IHttpActionResult> GetAllTransactions()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetTransactionBySKU([FromUri] string SKU)
        {
            Transaction transaction = await _context.Transactions.FindAsync(SKU);

            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();
        }





    }
}
