using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crud;
using Microsoft.AspNetCore.Mvc;

namespace Database.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICrud<Customer> _crud;
        public CustomerController(ShopContext context)
        {
            _crud = new Crud<Customer>(context);
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(_crud.GetAll());
        }


        [HttpGet("GetByID/{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            if (id == null || _crud.GetById(id) == null)
                return NotFound("ID :" + id + " Is not found");

            return Ok(_crud.GetById(id));
        }

        [HttpGet("Search={query}")]
        public ActionResult<IEnumerable<Customer>> Search(string query)
        {
            var result = _crud.Search(c => c.Name.Contains(query) 
            || c.Family.Contains(query) 
            || c.PhoneNumber.Contains(query) 
            || c.Email.Contains(query));

            if (result.Count() == 0)
                return NotFound("User is NotFound");

            return Ok(result);
        }

        [HttpPost("Create")]
        public ActionResult<Customer> Insert(Customer customer)
        {
            _crud.Insert(customer);
            _crud.SaveChanges();

            return Created("Created is successfully", customer);
        }

        [HttpPut("Update")]
        public ActionResult<Customer> Update(Customer customer)
        {
            try
            {
                _crud.Update(customer);
                _crud.SaveChanges();
            }
            catch
            {
                return NotFound("ID :" + customer.CustomerID + " Is not found");
            }

            return Ok(customer);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            if (id == null || _crud.GetById(id) == null)
                return NotFound();

            _crud.Delete(id);
            _crud.SaveChanges();

            return NoContent();
        }





    }
}