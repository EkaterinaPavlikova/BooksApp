using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreApplication.Infrastructure;
using CoreApplication.Models;
using CoreApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CoreApplication.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private IBookRepository bookRepository;
        private Cart cart;

        public OrderController(IOrderRepository repo, IBookRepository bookRepository, Cart cartService)
        {
            repository = repo;
            this.bookRepository = bookRepository;
            cart = cartService;
        }


        [HttpPost]
        [Authorize(Roles = Role.User + "," + Role.Admin)]
        public RedirectToActionResult AddBookToOrder(int BookId, int BookCount, string returnUrl)
        {
            Book book = bookRepository.Get(BookId);
            if (book.Count < BookCount)
            {
                TempData["message"] = "Не достаточно книг " + book.Title;
            }
            else
            {
                List<Order> orders = new List<Order>();
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                for (var i = 0; i < BookCount; i++)
                {
                    orders.Add(new Order
                    {
                        ReaderId = userId,
                        BookId = BookId,
                        Status = BookStatus.Processing
                    });
                }

                book.Count -= BookCount;


                if (orders.Count() > 0)
                {
                    repository.Create(orders);
                    repository.Save();
                    bookRepository.Update(book);
                    bookRepository.Save();
                    cart.RemoveLine(book);
                }
                else
                {
                    ModelState.AddModelError("", "Your cart is empty!");
                }
            }


            return RedirectToAction("Index", "Cart");
        }

        [Authorize(Roles = Role.User + "," + Role.Admin)]
        public RedirectToActionResult AddAllBooksToOrder()
        {
            List<Book> books = bookRepository.Books.ToList();
            List<Order> orders = new List<Order>();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
           
            string err = "";

            foreach(var line in cart.Line)
            {
                var book = books.Where(b => b.Id == line.Book.Id).FirstOrDefault();
                if (book == null)
                {
                    err += "Книга " + line.Book.Title + " отсутствует в базе";
                }
                    else
                {
                    if (book.Count < line.Quantity)
                    {
                        err += " \nНе достаточно книг " + book.Title + ". Вы взяли " + book.Count + " из" + line.Quantity;

                        for (var i = 0; i < book.Count; i++)
                        {
                            orders.Add(new Order
                            {
                                ReaderId = userId,
                                BookId = line.Book.Id,
                                Status = BookStatus.Processing
                            });
                        }

                        book.Count = 0;
                        continue;
                    }



                    for (var i = 0; i < line.Quantity; i++)
                    {
                        orders.Add(new Order
                        {
                            ReaderId = userId,
                            BookId = line.Book.Id,
                            Status = BookStatus.Processing
                        });
                    }
                    book.Count -= line.Quantity;
                }
            }
           
            if (orders.Count() > 0)
            {
                repository.Create(orders);
                repository.Save();
                bookRepository.Update(books);
                bookRepository.Save();
                cart.Clear();
            }

            
            TempData["message"] =  err;
            return RedirectToAction("UserOrderList");
        }


        [Authorize(Roles = Role.Librarian + "," + Role.Admin)]
        public IActionResult GiveBook(int orderId)
        {
            Order order = repository.GetOrderById(orderId);
            if (order != null)
            {
                order.Status = BookStatus.Issued;
                repository.Update(order);
                repository.Save();

            }
            
            return RedirectToAction("LibrarianList");
        }

        [Authorize(Roles = Role.Librarian + "," + Role.Admin)]
        public IActionResult TakeOrRefuseBook(int orderId, int bookId)
        {                         
            repository.Delete(orderId);
            repository.Save();

            Book book = bookRepository.Get(bookId);
            book.Count++;
            bookRepository.Update(book);
            bookRepository.Save();

          

            return RedirectToAction("LibrarianList");
        }


        [Authorize(Roles = Role.Librarian + "," + Role.Admin)]
        public ViewResult LibrarianList()
        {
            List<Order> orders = repository.Orders.Where(o => o.Status != BookStatus.Issued).ToList();
            return View(orders);

        }

        [Authorize(Roles = Role.User + "," + Role.Admin)]
        public IActionResult RemoveFromOrder(int orderId)
        {
            Order order = repository.GetOrderById(orderId);
            if (order != null)
            {
                order.Status = BookStatus.OnReturn;
                repository.Update(order);
                repository.Save();
            }

            return RedirectToAction("UserOrderList");
        }
        [Authorize(Roles = Role.User + "," + Role.Admin)]
        public IActionResult UserOrderList()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IQueryable<Order> orders, userOrders, prossesingOrders;
            try
            {
                orders = repository.Orders.Where(o => o.ReaderId.Equals(userId));
                userOrders = orders.Where(o => o.Status == BookStatus.Issued);
                prossesingOrders = orders?.Where(o => o.Status != BookStatus.Issued);
            }
            catch {
                userOrders = null;
                prossesingOrders = null;
            }
                      
            return View(
                new UserPageViewModel
                {
                    UserOrders = userOrders,
                    OrdersOnProsessing = prossesingOrders
                }
                );
        }
    }
}
