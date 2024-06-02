using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using crud;
using Database;
using MainProject.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Controllers
{

    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICrud<Customer> _customerDb;

        public AccountController(UserManager<IdentityUser> userManager
        , SignInManager<IdentityUser> signInManager, ShopContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerDb = new Crud<Customer>(context);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();


                var user = new IdentityUser()
                {
                    UserName = registerViewModel.Name,
                    PasswordHash = registerViewModel.Password,
                    Email = registerViewModel.Email,
                    PhoneNumber = registerViewModel.PhoneNumber,
                };

                var result = _userManager.CreateAsync(user, registerViewModel.Password).Result;

                var dateNow = DateTime.Now;
                if (result.Succeeded)
                {

                    _signInManager.SignInAsync(user, false);
                    _customerDb.Insert(new Customer()
                    {
                        Date = dateNow,
                        DeathTime = dateNow.AddMonths(5),
                        Email = user.Email,
                        Name = user.UserName,
                        Family = registerViewModel.Family,
                        PhoneNumber = user.PhoneNumber,
                        UserId = user.Id,
                    });
                    _customerDb.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }


                return RedirectToAction("Index", "Home");

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var result = _signInManager.PasswordSignInAsync(loginViewModel.Name, loginViewModel.Password, true, false).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }


            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            try
            {
                _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");

            }
            catch
            {
                return RedirectToAction("Index", "Home");

            }
        }



    }

}