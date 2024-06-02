using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using crud;
using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainProject.Controllers
{
    public class OrderController : Controller
    {
        private ICrud<ShopingCart> _shopingCartDb;
        private ICrud<Order> _orderDb;
        private ICrud<Customer> _customerDb;
        private ICrud<Ware> _wareDb;
        public OrderController(ShopContext context)
        {
            _shopingCartDb = new Crud<ShopingCart>(context);
            _orderDb = new Crud<Order>(context);
            _customerDb = new Crud<Customer>(context);
            _wareDb = new Crud<Ware>(context);
        }


        [HttpPost]
        public IActionResult MakeOrder(string delivaryPlace)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int customerId = _customerDb.Search(c => c.UserId == userId).First().CustomerID;

                var shopingCarts = _shopingCartDb.Search(c => c.CustomerID == customerId);
                foreach (var item in shopingCarts)
                {
                    _orderDb.Insert(new Order()
                    {
                        CustomerID = item.CustomerID,
                        Count = item.Count,
                        OrderDate = DateTime.Now,
                        DelivaryPlace = delivaryPlace,
                        WareID = item.WareID
                    });

                    var shopingCart = _shopingCartDb.GetById(item.ShopingCartID);
                    _shopingCartDb.Delete(shopingCart);

                    var ware = _wareDb.GetById(item.WareID);
                    var wareUpdated = new Ware()
                    {
                        WareID = ware.WareID,
                        WareGroupID = ware.WareGroupID,
                        WareSubGroupID = ware.WareSubGroupID,
                        Name = ware.Name,
                        Description = ware.Description,
                        Price = ware.Price,
                        DateOfInsert = ware.DateOfInsert,
                        Visit = ware.Visit,
                        Image = ware.Image,
                        Walpapers = ware.Walpapers,
                        Count = ware.Count - item.Count,
                    };

                    if (wareUpdated.Count <= 0)
                        wareUpdated.Count = 0;

                    _wareDb.Update(wareUpdated);


                    _shopingCartDb.SaveChanges();
                    _shopingCartDb.SaveChanges();
                    _orderDb.SaveChanges();
                }

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

        }

        [Area("Admin")]
        [HttpGet]
        public IActionResult GetOrders()
        {

            var allOrders = _orderDb.GetAll();
            ViewBag.Wares = _wareDb.GetAll();
            ViewBag.Customers = _customerDb.GetAll();
            return PartialView("GetOrders", allOrders);
        }

        [Area("Admin")]
        [HttpGet]
        public IActionResult GetCustomerInfo(int id)
        {
            var customer = _customerDb.GetById(id);

            return PartialView(customer);

        }

        [Area("Admin")]
        [HttpPost]
        public IActionResult DoneOrder(int id)
        {
            var order = _orderDb.GetById(id);
            _orderDb.Delete(order);
            _orderDb.SaveChanges();

            return RedirectToAction("GetOrders");

        }
    }


}