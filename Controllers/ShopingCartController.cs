using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using crud;
using Database;
using MainProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainProject.Controllers
{
    [Authorize]
    public class ShopingCartController : Controller
    {
        private ICrud<ShopingCart> _shopingCartDb;
        private ICrud<Database.Ware> _wareDb;
        private ICrud<Customer> _customerDb;
        private ICrud<Order> _orderDb;
        private ShopContext _db;

        private SignInManager<IdentityUser> _signInManager;

        public ShopingCartController(ShopContext context, SignInManager<IdentityUser> signinManager)
        {
            _db = context;
            _signInManager = signinManager;

            _shopingCartDb = new Crud<ShopingCart>(_db);
            _customerDb = new Crud<Customer>(_db);
            _wareDb = new Crud<Ware>(_db);
            _orderDb = new Crud<Order>(_db);
        }

        public IActionResult Index()
        {
            var wares = _wareDb.GetAll();

            var waresInShopingCart = _shopingCartDb.GetAll();
            ViewBag.IEnumrableWares = wares;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = _customerDb.Search(c => c.UserId == userId).First().CustomerID;

            return PartialView(waresInShopingCart.Where(c => c.CustomerID == customerId));
        }

        public IActionResult AddToBasket(int wareId)
        {
            // var wareInShopingCart = _shopingCartDb.Search(c => c.WareID == wareId).FirstOrDefault();


            var wares = _wareDb.GetAll();
            var ware = _wareDb.GetById(wareId);
            int wareCount = ware.Count;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = _customerDb.Search(c => c.UserId == userId).First().CustomerID;
            var shopingCartCustomer = _shopingCartDb.GetAll().FirstOrDefault(c => c.CustomerID == customerId);

            var wareInShopingCart = _shopingCartDb.Search(c => c.WareID == wareId).Where(c => c.CustomerID == customerId).FirstOrDefault();

            if (wareInShopingCart == null || shopingCartCustomer == null)
            {
                var shopingCart = new ShopingCart()
                {
                    WareID = wareId,
                    Count = 1,
                    CustomerID = customerId,
                    Price = ware.Price,
                };

                if (CheckWareHouse(wareCount, shopingCart.Count))
                    ViewBag.WareHouse = 0;


                else
                {
                    ViewBag.IEnumrableWares = wares;
                    ViewBag.GeneralGroup = _shopingCartDb.GetAll();
                    _shopingCartDb.Insert(shopingCart);
                    _shopingCartDb.SaveChanges();
                }


            }

            else
            {

                int shopingCartId = wareInShopingCart.ShopingCartID;
                var shopingCart = _shopingCartDb.GetById(shopingCartId);

                if (CheckWareHouse(wareCount, shopingCart.Count))
                    ViewBag.WareHouse = 0;

                else
                {
                    shopingCart.Count++;
                    _shopingCartDb.SaveChanges();
                }



            }

            return PartialView("_AddToBasket", _shopingCartDb.GetAll());

        }


        public IActionResult Remove(int wareId)
        {
            var wareInShopingCart = _shopingCartDb.Search(c => c.WareID == wareId).FirstOrDefault();
            var wares = _wareDb.GetAll();
            ViewBag.IEnumrableWares = wares;


            if (wareInShopingCart.Count <= 1)
                _shopingCartDb.Delete(wareInShopingCart);

            wareInShopingCart.Count--;



            _shopingCartDb.SaveChanges();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = _customerDb.Search(c => c.UserId == userId).First().CustomerID;

            return PartialView("Index", _shopingCartDb.Search(c => c.CustomerID == customerId));
        }

        [HttpPost]
        public IActionResult Delete(int shopingCartId)
        {
            var wares = _wareDb.GetAll();
            ViewBag.IEnumrableWares = wares;

            var wareInShopingCartId = _shopingCartDb.GetById(shopingCartId);
            _shopingCartDb.Delete(shopingCartId);
            _shopingCartDb.SaveChanges();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = _customerDb.Search(c => c.UserId == userId).First().CustomerID;

            return PartialView("Index", _shopingCartDb.Search(c => c.CustomerID == customerId));
        }



        public bool CheckWareHouse(int wareCount, int orderCount)
        {
            if (wareCount <= orderCount)
                return true;

            return false;

        }



    }
}