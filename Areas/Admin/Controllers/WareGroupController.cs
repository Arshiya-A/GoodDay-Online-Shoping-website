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
    public class WareGroupController : ControllerBase
    {
        private ICrud<WareGroup> _crud;
        public WareGroupController(ShopContext context)
        {
            _crud = new Crud<WareGroup>(context);
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<WareGroup>> GetAll()
        {
            return Ok(_crud.GetAll());
        }


        [HttpGet("GetByID/{id}")]
        public ActionResult<WareGroup> GetById(int id)
        {
            if (id == null || _crud.GetById(id) == null)
                return NotFound("ID :" + id + " Is not found");

            return Ok(_crud.GetById(id));
        }

        [HttpGet("Search={query}")]
        public ActionResult<IEnumerable<WareGroup>> Search(string query)
        {
            var result = _crud.Search(c => c.WareGroupName.Contains(query));

            if (result.Count() == 0)
                return NotFound("User is NotFound");

            return Ok(result);
        }

        [HttpPost("Create")]
        public ActionResult<WareGroup> Insert(WareGroup customer)
        {
            _crud.Insert(customer);
            _crud.SaveChanges();

            return Created("Created is successfully", customer);
        }

        [HttpPut("Update")]
        public ActionResult<WareGroup> Update(WareGroup customer)
        {
            try
            {
                _crud.Update(customer);
                _crud.SaveChanges();
            }
            catch
            {
                return NotFound("ID :" + customer.WareGroupID + " Is not found");
            }

            return Ok(customer);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<WareGroup> Delete(int id)
        {
            if (id == null || _crud.GetById(id) == null)
                return NotFound();

            _crud.Delete(id);
            _crud.SaveChanges();

            return NoContent();
        }

        [HttpPost("Delete")]
        public ActionResult<WareGroup> Delete(WareGroup wareGroup)
        {

            _crud.Delete(wareGroup);
            _crud.SaveChanges();

            return NoContent();
        }


    }
}