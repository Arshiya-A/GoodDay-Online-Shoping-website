using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using crud;
using Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageWareSubGroupController : Controller
    {
        private HttpClient _client;
        private const string URL = "http://localhost:5172/";

        public ManageWareSubGroupController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(URL);
        }

        public IActionResult All()
        {
            var response = _client.GetAsync("api/WareSubGroup/GetAll").Result;
            var all = response.Content.ReadFromJsonAsync<IEnumerable<WareSubgroup>>().Result;

            return PartialView(all);
        }

        public IActionResult Add()
        {
            var dropdownItems = new List<SelectListItem>();

            var wareSubGroupsResponse = _client.GetAsync("api/WareGroup/GetAll").Result;
            var wareSubGroups = wareSubGroupsResponse.Content.ReadFromJsonAsync<IEnumerable<WareGroup>>().Result;

            foreach (var item in wareSubGroups)
                dropdownItems.Add(new SelectListItem() { Text = item.WareGroupName, Value = item.WareGroupID.ToString() });

            ViewBag.DropdownItems = dropdownItems;

            return PartialView();
        }

        [HttpPost]
        public IActionResult Add(WareSubgroup wreSubgroup)
        {
            if (ModelState.IsValid)
            {
                var response = _client.PostAsJsonAsync("api/WareSubGroup/Create", wreSubgroup);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Edit(int id)
        {
            var wareGroupsResponse = _client.GetAsync($"api/WareGroup/GetAll").Result;
            var wareGroup = wareGroupsResponse.Content.ReadFromJsonAsync<IEnumerable<WareGroup>>().Result;

            var wareSubGroupResponse = _client.GetAsync($"api/WareSubGroup/GetById/{id}").Result;
            var wareSubGroup = wareSubGroupResponse.Content.ReadFromJsonAsync<WareSubgroup>().Result;


            var dropdownItems = new List<SelectListItem>();

            foreach (var item in wareGroup)
                dropdownItems.Add(new SelectListItem() { Text = item.WareGroupName, Value = item.WareGroupID.ToString() });

            ViewBag.DropdownItems = dropdownItems;

            return PartialView(wareSubGroup);

        }

        [HttpPost]
        public IActionResult Edit(WareSubgroup wareSubgroup)
        {
            if (ModelState.IsValid)
            {
                var response = _client.PutAsJsonAsync("api/WareSubGroup/Update", wareSubgroup);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int id)
        {
            var wareSubgroupsResponse = _client.GetAsync($"api/WareSubGroup/GetById/{id}").Result;
            var wareSubgroups = wareSubgroupsResponse.Content.ReadFromJsonAsync<WareSubgroup>().Result;

            return PartialView(wareSubgroups);
        }

        [HttpPost]
        public IActionResult FinallyDelete(int id)
        {
            var wareSubGroupResponse = _client.GetAsync($"api/WareSubGroup/GetByID/{id}").Result;
            var wareSubGroup = wareSubGroupResponse.Content.ReadFromJsonAsync<WareSubgroup>().Result;

            var wareSubGroupDeleteResponse = _client.PostAsJsonAsync($"api/WareSubGroup/Delete", wareSubGroup).Result;

            return RedirectToAction("Index", "Home");
        }
    }
}