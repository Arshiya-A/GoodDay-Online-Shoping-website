using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using crud;
using Database;
using MainProject.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ICrud<WareGroup> _wareGroupDb;
        private readonly ICrud<WareSubgroup> _wareSubGroupDb;
        private readonly ICrud<Ware> _wareDb;

        public HomeController(ShopContext context)
        {
            _wareGroupDb = new Crud<WareGroup>(context);
            _wareSubGroupDb = new Crud<WareSubgroup>(context);
            _wareDb = new Crud<Ware>(context);
        }

        public IActionResult Index()
        {
            var wareGroups = _wareGroupDb.GetAll();
            var wareSubGroups = _wareSubGroupDb.GetAll();
            var generalGroup = new GeneralGroup()
            {
                WareGroups = wareGroups,
                WareSubgroups = new List<WareSubgroup>()
            };
            ViewBag.Groups = generalGroup;

            var wares = _wareDb.GetAll();
            return View(wares);
        }


        public IActionResult Search(string query)
        {
            var groups = _wareGroupDb.GetAll();
            var subGroups = _wareSubGroupDb.GetAll();
            var generalGroup = new GeneralGroup()
            {
                WareGroups = groups,
                WareSubgroups = subGroups,
            };
            ViewBag.GeneralGroup = generalGroup;
            var allWares = _wareDb.GetAll();

            var results = _wareDb.Search(c => c.Name.Contains(query)
            || c.Description.Contains(query));

            return PartialView("Wares", results);
        }



        public IActionResult FilterByGroup(int id)
        {
            var wareGroups = _wareGroupDb.GetAll();
            var wareSubGroups = _wareSubGroupDb.GetAll();
            var generalGroup = new GeneralGroup()
            {
                WareGroups = wareGroups,
                WareSubgroups = wareSubGroups
            };
            ViewBag.Groups = generalGroup;

            var wares = _wareDb.Search(c => c.WareGroupID == id);
            return PartialView("Wares", wares);
        }

        public IActionResult ShowSubGroups(int id)
        {
            var wareGroups = _wareGroupDb.GetAll();
            var wareSubGroups = _wareSubGroupDb.Search(c => c.WareGroupID == id);
            var generalGroup = new GeneralGroup()
            {
                WareGroups = wareGroups,
                WareSubgroups = wareSubGroups
            };
            ViewBag.Groups = generalGroup;

            return PartialView("FilterSection", generalGroup);
        }


        public IActionResult FilterBySubGroup(int id)
        {
            var wares = _wareDb.Search(c => c.WareSubGroupID == id);
            return PartialView("Wares", wares);
        }





    }
}