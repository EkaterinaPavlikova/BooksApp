using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using CoreApplication.Infrastructure;
using CoreApplication.Models;
using CoreApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CoreApplication.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IBookRepository repository;
        private Cart cart;

        public CartController(IBookRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        [Authorize(Roles = Role.User + "," + Role.Admin)]
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel{
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        [Authorize(Roles = Role.User + "," + Role.Admin)]
        public RedirectToActionResult AddToCart(int Id, string returnUrl)
        {
            Book book = repository.Books.FirstOrDefault(b => b.Id == Id);

            if (book != null)
            {     
                cart.AddItem(book, 1);               
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        [Authorize(Roles = Role.User + "," + Role.Admin)]
        public RedirectToActionResult RemoveFromCart(int Id, string returnUrl)
        {
            Book book = repository.Books.FirstOrDefault(b => b.Id == Id);
            if (book != null) {              
                cart.RemoveLine(book);              
            }
            return RedirectToAction("Index", new { returnUrl });
        }
      
    }
}