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
    public class WareController : ControllerBase
    {
        private ICrud<Ware> _crud;
        public WareController(ShopContext context)
        {
            _crud = new Crud<Ware>(context);
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Ware>> GetAll()
        {
            return Ok(_crud.GetAll());
        }


        [HttpGet("GetByID/{id}")]
        public ActionResult<Ware> GetById(int id)
        {
            if (id == null || _crud.GetById(id) == null)
                return NotFound("ID :" + id + " Is not found");

            return Ok(_crud.GetById(id));
        }

        [HttpGet("Search={query}")]
        public ActionResult<IEnumerable<Ware>> Search(string query)
        {
            var result = _crud.Search(c => c.Name.Contains(query));

            if (result.Count() == 0)
                return NotFound("User is NotFound");

            return Ok(result);
        }

        [HttpPost("Create")]
        public ActionResult<Ware> Insert(Ware ware)
        {
            _crud.Insert(ware);
            _crud.SaveChanges();

            return Created("Created is successfully", ware);
        }

        [HttpPut("Update")]
        public ActionResult<Ware> Update(Ware ware)
        {
            try
            {
                _crud.Update(ware);
                _crud.SaveChanges();
            }
            catch
            {
                return NotFound("ID :" + ware.WareID + " Is not found");
            }

            return Ok(ware);
        }


        [HttpPost("Delete")]
        public ActionResult<WareGroup> Delete(Ware ware)
        {

            _crud.Delete(ware);
            _crud.SaveChanges();

            return NoContent();
        }

    }
}