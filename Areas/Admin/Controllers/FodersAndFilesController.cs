using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Image.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FodersAndFilesController : ControllerBase
    {
        private IHostEnvironment _hostEnvironment;

        public FodersAndFilesController(IHostEnvironment environment)
        {
            _hostEnvironment = environment;
        }

        [HttpPost("PrepareFolders")]
        public ActionResult PrepareFolders(int id)
        {
            string mainDirectory = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
            string folderDirectory = Path.Combine(mainDirectory, "Ware-" + id);
            string wallpaperDirectory = Path.Combine(folderDirectory, "Images");

            if (!Directory.Exists(mainDirectory))
                Directory.CreateDirectory(mainDirectory);

            if (!Directory.Exists(folderDirectory))
                Directory.CreateDirectory(folderDirectory);

            if (!Directory.Exists(wallpaperDirectory))
                Directory.CreateDirectory(wallpaperDirectory);

            return Ok();
        }
    }
}