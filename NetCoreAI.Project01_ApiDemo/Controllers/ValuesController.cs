using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project01_ApiDemo.Context;
using NetCoreAI.Project01_ApiDemo.Entities;
namespace NetCoreAI.Project01_ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApiContext _context;

        public ValuesController(ApiContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult CustomerList()//Listeleme
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)//Veri Ekleme
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Customer added successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)//Veri Silme
        {
            var customer = _context.Customers.Find(id);
            
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok("Customer deleted successfully");
        }

        [HttpGet("GetCustomer")]
        public IActionResult GetCustomer(int id)//İd ye göre çağırma
        {
            var customer = _context.Customers.Find(id);
            return Ok(customer);
        }

        [HttpPut]
        public IActionResult UpdateCustomer(Customer customer)//Veri Güncelleme
        {
            var existingCustomer = _context.Customers.Find(customer.CustomerId);
            if (existingCustomer == null)
            {
                return NotFound("Customer not found");
            }
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.CustomerSurname = customer.CustomerSurname;
            existingCustomer.CustomerBalance = customer.CustomerBalance;
            _context.SaveChanges();
            return Ok("Customer updated successfully");
        }

    }
}
