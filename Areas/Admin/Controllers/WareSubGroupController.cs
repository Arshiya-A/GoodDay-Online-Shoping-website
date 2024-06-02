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
    public class WareSubGroupController : ControllerBase
    {
        private ICrud<WareSubgroup> _crud;
        public WareSubGroupController(ShopContext context)
        {
            _crud = new Crud<WareSubgroup>(context);
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<WareSubgroup>> GetAll()
        {
            return Ok(_crud.GetAll());
        }


        [HttpGet("GetByID/{id}")]
        public ActionResult<WareSubgroup> GetById(int id)
        {
            if (id == null || _crud.GetById(id) == null)
                return NotFound("ID :" + id + " Is not found");

            return Ok(_crud.GetById(id));
        }

        [HttpGet("Search={query}")]
        public ActionResult<IEnumerable<WareSubgroup>> Search(string query)
        {
            var result = _crud.Search(c => c.WareSubGroupName.Contains(query));

            if (result.Count() == 0)
                return NotFound("User is NotFound");

            return Ok(result);
        }

        [HttpPost("Create")]
        public ActionResult<Customer> Insert(WareSubgroup wareSubgroup)
        {
            _crud.Insert(wareSubgroup);
            _crud.SaveChanges();

            return Created("Created is successfully", wareSubgroup);
        }

        [HttpPut("Update")]
        public ActionResult<Customer> Update(WareSubgroup wareSubgroup)
        {
            try
            {
                _crud.Update(wareSubgroup);
                _crud.SaveChanges();
            }
            catch
            {
                return NotFound("ID :" + wareSubgroup.WareSubGroupID + " Is not found");
            }

            return Ok(wareSubgroup);
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

        [HttpPost("Delete")]
        public ActionResult<Customer> Delete(WareSubgroup wareSubgroup)
        {

            _crud.Delete(wareSubgroup);
            _crud.SaveChanges();

            return NoContent();
        }




    }
}