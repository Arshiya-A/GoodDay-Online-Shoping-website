using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using crud;
using Database;
using MainProject.Admin.ViewModels;
using MainProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace MainProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageWareGroupController : Controller
    {

        private HttpClient _client;
        private const string URL = "http://localhost:5172/";

        public ManageWareGroupController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(URL);
        }

        public IActionResult All()
        {
            var response = _client.GetAsync("api/WareGroup/GetAll").Result;
            var all = response.Content.ReadFromJsonAsync<IEnumerable<WareGroup>>().Result;

            return PartialView("All", all);
        }


        public IActionResult Add()
        {
            return PartialView();
        }


        [HttpPost]
        public IActionResult Add(WareGroup wareGroup)
        {
            if (ModelState.IsValid)
            {
                var response = _client.PostAsJsonAsync("api/WareGroup/Create", wareGroup).Result;
            }

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Edit(int id)
        {
            var response = _client.GetAsync($"api/WareGroup/GetByID/{id}").Result;
            var wareGroup = response.Content.ReadFromJsonAsync<WareGroup>().Result;
            return PartialView(wareGroup);
        }

        [HttpPost]
        public IActionResult Edit(WareGroup wareGroup)
        {
            if (ModelState.IsValid)
            {
                var response = _client.PutAsJsonAsync("api/WareGroup/Update", wareGroup).Result;
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Delete(int id)
        {
            var wareGroupResponse = _client.GetAsync($"api/WareGroup/GetByID/{id}").Result;
            var wareGroup = wareGroupResponse.Content.ReadFromJsonAsync<WareGroup>().Result;

            var wareSubgroupsResponse = _client.GetAsync($"api/WareSubGroup/GetAll").Result;
            var wareSubgroups = wareSubgroupsResponse.Content.ReadFromJsonAsync<IEnumerable<WareSubgroup>>().Result;


            if (wareSubgroups.Where(c => c.WareGroupID == wareGroup.WareGroupID).Count() > 0)
            {
                return PartialView("Error", new Error() { Reason = "This Group have Subgroup !" });
            }
            return PartialView(wareGroup);
        }

        [HttpPost]
        public IActionResult FinallyDelete(int id)
        {
            var wareGroupResponse = _client.GetAsync($"api/WareGroup/GetByID/{id}").Result;
            var wareGroup = wareGroupResponse.Content.ReadFromJsonAsync<WareGroup>().Result;

            var wareGroupDeleteResponse = _client.PostAsJsonAsync($"api/WareGroup/Delete", wareGroup).Result;

            return RedirectToAction("Index", "Home");
        }
    }
}