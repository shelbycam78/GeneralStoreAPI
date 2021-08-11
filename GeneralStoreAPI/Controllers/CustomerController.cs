using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
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
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);               

                return Ok("Customer has been created");
            }

            return BadRequest(ModelState);

        }

    }
}
