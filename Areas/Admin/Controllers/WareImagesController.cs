using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Image.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WareImagesController : ControllerBase
    {
        private IHostingEnvironment _hostEnvironment;
        public WareImagesController(IHostingEnvironment environment)
        {
            _hostEnvironment = environment;

        }

        [HttpGet("GetMainImage")]
        public ActionResult<string> GetMainImage(int wareId)
        {

            string mainDirectory = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            string mainImageDirectory = Path.Combine(mainDirectory, "Ware-" + wareId);
            string mainImagePath = Path.Combine(mainImageDirectory, $"ware-{wareId}.png");

            if (!System.IO.File.Exists(mainImagePath))
                return NotFound("This image in not exits");

            else
                return Ok(mainImagePath);

        }

        [HttpGet("GetImages")]
        public ActionResult<IEnumerable<string>> GetImages(int wareId)
        {
            string mainDirectory = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            string mainImageDirectory = Path.Combine(mainDirectory, "Ware-" + wareId);
            string imagesDirectory = Path.Combine(mainImageDirectory, "Images");

            int imageCount = -1;
            List<string> paths = new List<string>();
            while (true)
            {
                imageCount++;
                string imagesFile = Path.Combine(imagesDirectory, $"ware-images-{wareId}I{imageCount}.png");

                if (!System.IO.File.Exists(imagesFile))
                    break;

                paths.Add(imagesFile);

            }


            return Ok(paths);
        }

        // [HttpPost("UploadMainImage")]
        // public ActionResult UploadMainImage(int wareId, string imagePath)
        // {

        //     // D:\MyHTMLtemplates\Stores\GoodDay\img\5bbc00e30f8a2-aaede6acf259f5af3df4cbe6aa3d01d4.png

        //     string mainDirectory = Path.Combine(_hostEnvironment.WebRootPath, "Images");
        //     string mainImageDirectory = Path.Combine(mainDirectory, "Ware-" + wareId);
        //     string mainImagePath = Path.Combine(mainImageDirectory, $"ware-{wareId}.png");


        //     if (!System.IO.File.Exists(mainImagePath))
        //     {
        //         try
        //         {
        //             System.IO.File.Create(mainImagePath).Dispose();

        //         }
        //         catch (System.IO.DirectoryNotFoundException)
        //         {
        //             return NotFound("This Directory is not exist");
        //         }

        //     }

        //     System.IO.File.Copy(imagePath, mainImagePath, true);
        //     return Ok("Succesfuly Created in [ " + mainImagePath + " ]");

        // }

        [HttpPost("UploadMainImage")]
        public ActionResult UploadMainImage(int wareId, IFormFile imageFile)
        {

            // D:\MyHTMLtemplates\Stores\GoodDay\img\5bbc00e30f8a2-aaede6acf259f5af3df4cbe6aa3d01d4.png

            string mainDirectory = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            string mainImageDirectory = Path.Combine(mainDirectory, "Ware-" + wareId);
            string mainImagePath = Path.Combine(mainImageDirectory, $"ware-{wareId}.png");


            // if (!System.IO.File.Exists(mainImagePath))
            // {
            //     try
            //     {
            //         System.IO.File.Create(mainImagePath).Dispose();

            //     }
            //     catch (System.IO.DirectoryNotFoundException)
            //     {
            //         return NotFound("This Directory is not exist");
            //     }

            // }

            using (var fs = new FileStream(mainImageDirectory, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                imageFile.CopyTo(fs);
            }
            return Ok("Succesfuly Created in [ " + mainImagePath + " ]");

        }



        [HttpPost("UploadImages")]
        public ActionResult UploadImages(int wareId, string imagesPath)
        {

            // D:\MyHTMLtemplates\Stores\GoodDay\img\5bbc00e30f8a2-aaede6acf259f5af3df4cbe6aa3d01d4.png
            // D:\myhtmltemplates\stores\goodDay\img\barbecue-nachos-lays-potato-chip-frito-lay-original-chips-184-2g-f4c6b825194e6be05a1b4813452503ea.png
            // D:\myhtmltemplates\stores\goodDay\img\cox-s-honey-bee-drink-gift-pack-f03188541bfbf4dd68753ab006f2a1b6.png
            // D:\myhtmltemplates\stores\goodDay\img\laptop-dell-toshiba-satellite-ultrabook-central-processing-unit-laptop-png-50172509d41f796ad4723f8367603356.png
            string[] paths = imagesPath.Split("|-|");
            string mainDirectory = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            string mainImageDirectory = Path.Combine(mainDirectory, "Ware-" + wareId);
            string imagesDirectory = Path.Combine(mainImageDirectory, "Images");

            string imagesFile = Path.Combine(imagesDirectory, $"ware-images-{wareId}");

            if (!Directory.Exists(imagesDirectory))
                System.IO.Directory.CreateDirectory(imagesDirectory);

            if (!System.IO.File.Exists(imagesFile))
            {
                try
                {
                    for (int i = 0; i < paths.Count(); i++)
                    {
                        string image = imagesFile + "I" + i + ".png";
                        System.IO.File.Create(image).Dispose();
                        System.IO.File.Copy(paths[i], image, true);
                    }

                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    return NotFound("This Directory is not exist");
                }
            }


            return Ok("Succesfuly Created Images");

        }

        [HttpPut("ChangeMainImage")]
        public ActionResult ChangeMainImage(int wareId, string newImagePath)
        {
            string mainDirectory = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            string mainImageDirectory = Path.Combine(mainDirectory, "Ware-" + wareId);
            string mainImagePath = Path.Combine(mainImageDirectory, $"ware-{wareId}.png");
            try
            {
                System.IO.File.Copy(newImagePath, mainImagePath, true);
            }
            catch (System.IO.FileNotFoundException)
            {
                return NotFound("This File is not exist");
            }

            catch (System.IO.DirectoryNotFoundException)
            {
                return NotFound("This Directory is not exist");
            }

            return NoContent();
        }

        [HttpPut("ChangeImage")]
        public ActionResult ChangeImage(int wareId, int selectId, string newImagePath)
        {
            string mainDirectory = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            string mainImageDirectory = Path.Combine(mainDirectory, "Ware-" + wareId);
            string imagesDirectory = Path.Combine(mainImageDirectory, "Images");
            string imagesFile = Path.Combine(imagesDirectory, $"ware-images-{wareId}I{selectId}.png");

            try
            {
                System.IO.File.Copy(newImagePath, imagesFile, true);
            }
            catch (System.IO.FileNotFoundException)
            {
                return NotFound("This File is not exist");
            }

            catch (System.IO.DirectoryNotFoundException)
            {
                return NotFound("This Directory is not exist");
            }

            return NoContent();
        }

        [HttpDelete("DeleteImageFolder")]
        public ActionResult DeleteImageFolder(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                return NotFound("This Directory is not exist");
            }

            return NoContent();
        }

        [HttpDelete("DeleteImage")]
        public ActionResult DeleteImage(int wareId, int imageId)
        {
            string mainDirectory = Path.Combine(_hostEnvironment.WebRootPath, "Images");
            string mainImageDirectory = Path.Combine(mainDirectory, "Ware-" + wareId);
            string imagesDirectory = Path.Combine(mainImageDirectory, "Images");
            string imagesFile = Path.Combine(imagesDirectory, $"ware-images-{wareId}I{imageId}.png");
            try
            {
                System.IO.File.Delete(imagesFile);
            }
            catch (System.IO.FileNotFoundException)
            {
                return NotFound("This Image is not exist");
            }

            catch (System.IO.DirectoryNotFoundException)
            {
                return NotFound("This Id is not exist");
            }

            return NoContent();
        }

    }
}