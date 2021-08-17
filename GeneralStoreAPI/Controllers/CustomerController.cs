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
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //POST
        //api/Customer
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer is null)
            {
                return BadRequest("Your request body can't be empty");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            int changecount = await _context.SaveChangesAsync();

            if (changecount < 1)
            {
                return InternalServerError();
            }

            return Ok("Customer has been created");
            
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCustomers()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerById([FromUri]int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            //check ids match
            if (id != updatedCustomer?.Id)
            {
                return BadRequest("Ids do not match");
            }

            //check model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //find customer in database
            Customer customer = await _context.Customers.FindAsync(id);

            //customer doesn't exist
            if (customer is null)
            {
                return NotFound();
            }

            //update customer
            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;

            //save changes
            //check to see if greater than zero.  same as above
            int changecount = await _context.SaveChangesAsync();
            if (changecount < 1)
            {
                return InternalServerError();
            }

            return Ok("The customer was updated.");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomer([FromUri] int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);

            int changecount = await _context.SaveChangesAsync();
            if (changecount <1)
            {
                return InternalServerError();
            }
            
            return Ok("Customer was deleted");                      
        }
    }
}
