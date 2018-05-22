using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreApplication.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreApplication.Infrastructure;

namespace CoreApplication.Controllers
{
    [Authorize]
    public class BookController: Controller
    {
        private IBookRepository repository;
        private IAuthorRepository authorRepositore;
        private IGenreRepository genreRepository;


        public int PageSize = 2;
        public int PageSizeForEditPage = 7;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepositore, IGenreRepository genreRepository)
        {           
            repository = bookRepository;
            this.authorRepositore = authorRepositore;
            this.genreRepository = genreRepository;
        }

        [Authorize(Roles = Role.User + "," + Role.Admin)]
        public IActionResult BooksList(string searchString = "",   int Page = 1, int? genre = null)
        {
         
             IQueryable<Book> books = repository.Books.Where(p => genre == null || p.GenreId == genre).Where(b => b.Count > 0);
             if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.Name.Contains(searchString) || b.Genre.GenreName.Contains(searchString) || b.Year.ToString().Contains(searchString));                                          
            }


            return View(new BooksListViewModel {
                            Books =  books.OrderBy(b => b.Id)
                                                  .Skip((Page - 1) * PageSize)
                                                  .Take(PageSize),

                            PagingInfo = new PagingInfo
                            {
                                CurrentPage = Page,
                                ItemsPerPage = PageSize,
                                TotalItems = books.Count()
                            },
                            CurrentGenreId = genre,
                            SearchingString = searchString
            });
        }

        [Authorize(Roles = Role.Storekeeper + "," + Role.Admin)]
        public IActionResult BooksEditPage(int p = 1)
        {
            var books = repository.Books;
            return View(new BooksListViewModel { 
                Books = books.OrderBy(b => b.Id)
                                                  .Skip((p - 1) * PageSizeForEditPage)
                                                  .Take(PageSizeForEditPage),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = p,
                    ItemsPerPage = PageSizeForEditPage,
                    TotalItems = books.Count()
                }

            });
        }

        [Authorize(Roles = Role.Storekeeper + "," + Role.Admin)]
        public ViewResult Edit(int bookId) => 
            View(new BookViewModel
            {
                Book = repository.Get(bookId),
                Authors = authorRepositore.Author, 
                Genres =  genreRepository.Genres
            });

        [Authorize(Roles = Role.Storekeeper + "," + Role.Admin)]
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.Update(book);
                repository.Save();

                return RedirectToAction("BooksEditPage");
            }
            {
                return View(book);
            }
        }

        [Authorize(Roles = Role.Storekeeper + "," + Role.Admin)]
        public ViewResult Create() =>
           View(new BookViewModel
           {
               Book = new Book(),
               Authors = authorRepositore.Author,
               Genres = genreRepository.Genres
           });

        [HttpPost]
        [Authorize(Roles = Role.Storekeeper + "," + Role.Admin)]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.Create(book);
                repository.Save();

                return RedirectToAction("BooksEditPage");
            }
            {
                return View(book);
            }
        }

        [Authorize(Roles = Role.Storekeeper + "," + Role.Admin)]
        public IActionResult Delete(int bookId)
        {
            Book book = repository.Get(bookId);
            if(book!= null)
            {
                repository.Delete(bookId);
                repository.Save();
            }
            
            return RedirectToAction("BooksEditPage");
        }

    }
}
