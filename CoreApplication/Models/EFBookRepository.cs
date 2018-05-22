using System;
using System.Collections.Generic;
using System.Linq;
using CoreApplication.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreApplication.Models
{
    public class EFBookRepository: IBookRepository, IGenreRepository, IAuthorRepository
    {
        private BookContext context;
        public EFBookRepository(BookContext bkContext)
        {
            context = bkContext;
        }
        public IQueryable<Book> Books => context.Books
                                                .Include(b=>b.Author)
                                                .Include(b=>b.Genre);
        
        public IQueryable<Genre> Genres => context.Genres;

        public IQueryable<Author> Author => context.Authors;

        public void Create(Book book)
        {
           context.Add(book);
        }

        public void Delete(int id)
        {
           Book book = context.Books.Find(id);
            if (book != null)
                context.Books.Remove(book);
        }

        public Book Get(int id)
        {
            return context.Books.Where(b => b.Id == id)
                .Include(b=>b.Author)
                .Include(b=>b.Genre)
                .FirstOrDefault();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
            
        }

        public void Update(List<Book> books)
        {

            foreach(var book in books)
            context.Entry<Book>(book).State = EntityState.Modified;

        }

       
    }
}
