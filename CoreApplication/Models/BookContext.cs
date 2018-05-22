using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreApplication.Models
{
    public class BookContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres{ get; set; }
        public DbSet<Author> Authors{get; set; }
        public DbSet<Order> Orders { get; set; }

        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
        }

       
    }
}
