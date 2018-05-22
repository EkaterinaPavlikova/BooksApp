using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreApplication.Models;
using CoreApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CoreApplication.Infrastructure;

namespace CoreApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private IServiceProvider _service;

        

        public AccountController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, RoleManager<IdentityRole> roleManager, IServiceProvider services)
        {        
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _service = services;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
             if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }             

            }
            return View(model);
        }

        [Authorize]
        public IActionResult Index()
        {
          
            var currentRole = User.Claims.Where(c => c.Type == ClaimsIdentity.DefaultRoleClaimType).Select(c => c.Value).FirstOrDefault();
            switch(currentRole)
            {
                case Role.User:
                    return RedirectToAction("BooksList", "Book");
                case Role.Admin:
                    return RedirectToAction("BooksList", "Book");
                case Role.Librarian:
                    return RedirectToAction("LibrarianList", "Order");
                case Role.Storekeeper:
                    return RedirectToAction("BooksEditPage", "Book");
                default:
                    return RedirectToAction("Login", "Account");
            }          
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = new UserIdentity { UserName = model.Login };             
                var result = await _userManager.CreateAsync(user, model.Password);
              

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Role.User);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }   

            }
            return View(model);
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {       
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();         
            return RedirectToAction("Login", "Account");
        }

    }
}