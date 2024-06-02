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
    public class OrderController : ControllerBase
    {
        private ICrud<Order> _crud;
        public OrderController(ShopContext context)
        {
            _crud = new Crud<Order>(context);
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
            var result = _crud.Search(c => c.DelivaryPlace.Contains(query));

            if (result.Count() == 0)
                return NotFound("Order is NotFound");

            return Ok(result);
        }

        [HttpPost("Create")]
        public ActionResult<Customer> Insert(Order order)
        {
            _crud.Insert(order);
            _crud.SaveChanges();

            return Created("Created is successfully", order);
        }

        [HttpPut("Update")]
        public ActionResult<Customer> Update(Order order)
        {
            try
            {
                _crud.Update(order);
                _crud.SaveChanges();
            }
            catch
            {
                return NotFound("ID :" + order.OrderID + " Is not found");
            }

            return Ok(order);
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