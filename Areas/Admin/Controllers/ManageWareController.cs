using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Database;
using MainProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Protocol;
using MainProject.Tools;
using Microsoft.AspNetCore.Authorization;

namespace MainProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageWareController : Controller
    {
        private HttpClient _client;
        private const string URL = "http://localhost:5172/";
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public ManageWareController(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(URL);
            _environment = environment;
        }


        public IActionResult All()
        {

            var response = _client.GetAsync("api/ware/GetAll").Result;
            var wares = response.Content.ReadFromJsonAsync<IEnumerable<Ware>>().Result;

            return PartialView("Wares", wares);
        }


        public IActionResult Add()
        {
            var dropdownItems = new List<SelectListItem>();

            var wareSubGroupsResponse = _client.GetAsync("api/WareSubGroup/GetAll").Result;
            var wareSubGroups = wareSubGroupsResponse.Content.ReadFromJsonAsync<IEnumerable<WareSubgroup>>().Result;

            foreach (var item in wareSubGroups)
                dropdownItems.Add(new SelectListItem() { Text = item.WareSubGroupName, Value = item.WareSubGroupID.ToString() });

            ViewBag.DropdownItems = dropdownItems;

            return PartialView();
        }


        [HttpPost]
        public IActionResult Add(WareViewModel wareViewModel)
        {
            try
            {
                wareViewModel.Visit++;
                wareViewModel.DateOfInsert = DateTime.Now;


                var ware = new Ware()
                {

                    WareSubGroupID = wareViewModel.WareSubGroupID,
                    Name = wareViewModel.Name,
                    Description = wareViewModel.Description,
                    Image = wareViewModel.Image.FileName,
                    Price = wareViewModel.Price,
                    DateOfInsert = wareViewModel.DateOfInsert,
                    Visit = wareViewModel.Visit,
                    Count = wareViewModel.Count,

                };

                var wareSubGroupResponse = _client.GetAsync($"api/WareSubGroup/GetByID/{ware.WareSubGroupID}").Result;
                var wareSubGroup = wareSubGroupResponse.Content.ReadFromJsonAsync<WareSubgroup>().Result;


                wareViewModel.WareGroupID = wareSubGroup.WareGroupID;
                ware.WareGroupID = wareViewModel.WareGroupID;
                UploadMainImage(wareViewModel);
                UploadWallpaper(wareViewModel);

                foreach (var item in wareViewModel.Walpapers)
                    ware.Walpapers += item.FileName + "|-| ";

                var response = _client.PostAsJsonAsync("api/Ware/Create", ware).Result;

                return RedirectToAction("Index", "Home");

            }
            catch
            {
                return RedirectToAction("Index", "Home");

            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var response = _client.GetAsync($"api/Ware/GetByID/{id}").Result;

                var ware = response.Content.ReadFromJsonAsync<Ware>().Result;
                var wareViewModel = new WareViewModel()
                {
                    Name = ware.Name,
                    Description = ware.Description,
                    Price = ware.Price,
                    DateOfInsert = ware.DateOfInsert,
                    Visit = ware.Visit,
                    WareGroupID = ware.WareGroupID,
                    WareSubGroupID = ware.WareSubGroupID,
                    WareID = ware.WareID,
                    Count = ware.Count,
                };

                Storage.Text = wareViewModel.Name;

                var wareSubGroupsResponse = _client.GetAsync("api/WareSubGroup/GetAll").Result;
                var wareSubGroups = wareSubGroupsResponse.Content.ReadFromJsonAsync<IEnumerable<WareSubgroup>>().Result;

                var dropdownItems = new List<SelectListItem>();

                foreach (var item in wareSubGroups)
                    dropdownItems.Add(new SelectListItem() { Text = item.WareSubGroupName, Value = item.WareSubGroupID.ToString() });

                ViewBag.DropdownItems = dropdownItems;

                return PartialView(wareViewModel);

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult Edit(WareViewModel wareViewModel)
        {
            try
            {
                var ware = new Ware()
                {
                    WareID = wareViewModel.WareID,
                    WareSubGroupID = wareViewModel.WareSubGroupID,
                    WareGroupID = wareViewModel.WareGroupID,
                    Name = wareViewModel.Name,
                    Description = wareViewModel.Description,
                    Image = wareViewModel.Image.FileName,
                    Price = wareViewModel.Price,
                    DateOfInsert = wareViewModel.DateOfInsert,
                    Visit = wareViewModel.Visit,
                    Count = wareViewModel.Count,
                };
                foreach (var item in wareViewModel.Walpapers)
                    ware.Walpapers += item.FileName + "|-| ";

                DeleteAllImages(ware.WareGroupID, ware.WareSubGroupID, Storage.Text);

                UploadMainImage(wareViewModel);
                UploadWallpaper(wareViewModel);

                var response = _client.PutAsJsonAsync("api/Ware/Update", ware).Result;
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index", "Home");
        }


        public IActionResult Delete(int id)
        {
            var wareResponse = _client.GetAsync($"api/Ware/GetByID/{id}").Result;
            var ware = wareResponse.Content.ReadFromJsonAsync<Ware>().Result;

            return PartialView(ware);
        }

        [HttpPost]
        public IActionResult FinallyDelete(int id)
        {
            var wareResponse = _client.GetAsync($"api/Ware/GetByID/{id}").Result;
            var ware = wareResponse.Content.ReadFromJsonAsync<Ware>().Result;

            DeleteAllImages(ware.WareGroupID, ware.WareSubGroupID, ware.Name);
            var wareDeleteResponse = _client.PostAsJsonAsync($"api/Ware/Delete", ware).Result;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowWallpaper(int id)
        {
            var wareResponse = _client.GetAsync($"api/Ware/GetByID/{id}").Result;
            var ware = wareResponse.Content.ReadFromJsonAsync<Ware>().Result;

            var wallpaper = new Wallpaper();

            wallpaper.Directory = Path.Combine("/Images/",
            ware.WareGroupID + "--" + ware.WareSubGroupID + "--" + ware.Name);

            var images = ware.Walpapers.Split("|-|");

            List<string> trimImages = new List<string>();
            foreach (var item in images)
            {
                trimImages.Add(item.TrimStart());
            }
            wallpaper.Images = trimImages;
            wallpaper.Images.RemoveAt(trimImages.Count - 1);

            return PartialView("Slider", wallpaper);
        }

        public void DeleteAllImages(int group, int subGroup, string wareName)
        {
            string mainPath = Path.Combine(_environment.WebRootPath, "Images", group + "--" + subGroup + "--" + wareName);

            if (Directory.Exists(mainPath))
                Directory.Delete(mainPath, true);
        }

        public void UploadMainImage(WareViewModel wareViewModel)
        {

            string imageDirectorysPath = Path.Combine(_environment.WebRootPath,
            "Images", wareViewModel.WareGroupID + "--" + wareViewModel.WareSubGroupID + "--" + wareViewModel.Name);

            string mainImageName = Path.Combine(imageDirectorysPath, wareViewModel.Image.FileName);

            if (!Directory.Exists(imageDirectorysPath))
                Directory.CreateDirectory(imageDirectorysPath);

            System.IO.File.Create(mainImageName).Dispose();

            using (var fs = new FileStream(mainImageName, FileMode.OpenOrCreate))
            {
                wareViewModel.Image.CopyTo(fs);
            }
        }

        public void UploadWallpaper(WareViewModel wareViewModel)
        {
            string imagesDirectorysPath = Path.Combine(_environment.WebRootPath, "Images",
            wareViewModel.WareGroupID + "--" + wareViewModel.WareSubGroupID + "--" + wareViewModel.Name, "Wallpaper");

            if (!Directory.Exists(imagesDirectorysPath))
                Directory.CreateDirectory(imagesDirectorysPath);

            string image = "";
            int index = -1;
            foreach (var item in wareViewModel.Walpapers)
            {
                index++;
                image = Path.Combine(imagesDirectorysPath, item.FileName);
                System.IO.File.Create(image).Dispose();

                using (var fs = new FileStream(image, FileMode.OpenOrCreate))
                {
                    item.CopyTo(fs);
                }
            }
        }

    }
}