using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MainProject.Models;
using crud;
using Database;
using MainProject.ViewModels;
using System.Security.Claims;

namespace MainProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ICrud<Ware> _wareDb;
    private ICrud<WareGroup> _wareGroupDb;
    private ICrud<WareSubgroup> _wareSubgroupDb;
    private ICrud<ShopingCart> _shopingCartDb;
    private ICrud<Customer> _customerDb;



    public HomeController(ILogger<HomeController> logger, ShopContext context)
    {
        _logger = logger;

        _wareDb = new Crud<Ware>(context);
        _wareGroupDb = new Crud<WareGroup>(context);
        _wareSubgroupDb = new Crud<WareSubgroup>(context);
        _shopingCartDb = new Crud<ShopingCart>(context);
        _customerDb = new Crud<Customer>(context);

    }

    public IActionResult Index()
    {
        CheckCustomerDeathDate();

        var groups = _wareGroupDb.GetAll();
        var subGroups = _wareSubgroupDb.GetAll();
        var generalGroup = new GeneralGroup()
        {
            WareGroups = groups,
            WareSubgroups = subGroups,
        };
        ViewBag.GeneralGroup = generalGroup;

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != null)
        {
            int customerId = _customerDb.Search(c => c.UserId == userId).First().CustomerID;
            var shopCart = _shopingCartDb.Search(c => c.CustomerID == customerId);

            ViewBag.ShoingCart = shopCart;

        }


        var allWares = _wareDb.GetAll();
        return View(allWares);
    }

    public IActionResult Home()
    {
        var allWares = _wareDb.GetAll();
        return PartialView(allWares);
    }

    public IActionResult WarePage(int id)
    {
        if (id == 0)
            return NotFound();

        var ware = _wareDb.GetById(id);
        ware.Visit++;
        _wareDb.SaveChanges();
        var subGroup = _wareSubgroupDb.Search(c => c.WareSubGroupID == ware.WareSubGroupID).First();
        ViewBag.subGroup = subGroup.WareSubGroupName;

        if (ware == null)
            return NotFound();

        return PartialView("_WarePage", ware);
    }


    public IActionResult Search(string query)
    {
        var groups = _wareGroupDb.GetAll();
        var subGroups = _wareSubgroupDb.GetAll();
        var generalGroup = new GeneralGroup()
        {
            WareGroups = groups,
            WareSubgroups = subGroups,
        };
        ViewBag.GeneralGroup = generalGroup;
        var allWares = _wareDb.GetAll();

        var results = _wareDb.Search(c => c.Name.Contains(query)
        || c.Description.Contains(query));

        return PartialView("_SearchResult", results);
    }

    public IActionResult GoToSubGroup(int wareSubId)
    {
        var wares = _wareDb.Search(c => c.WareSubGroupID == wareSubId);
        return PartialView("_GoToSubGroup", wares);
    }

    public void CheckCustomerDeathDate()
    {
        foreach (var item in _customerDb.GetAll())
        {
            if (item.DeathTime == DateTime.Now)
            {
                _customerDb.Delete(item);
                _customerDb.SaveChanges();
            }
        }
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
