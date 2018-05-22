using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApplication.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApplication.Models
{
    public static class SeedData
    {
        public static void InitializeDB(IApplicationBuilder app) {
            BookContext context = app.ApplicationServices.GetRequiredService<BookContext>();
            
            
            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book
                    {
                        Title = "BookA",
                        Year = 2000,
                        GenreId = 3,
                        AuthorId = 6
                    },
                    new Book
                    {
                        Title = "BookB",
                        Year = 2001,
                        GenreId = 6,
                        AuthorId = 6
                    },
                    new Book
                    {
                        Title = "BookC",
                        Year = 2002,
                        GenreId = 1,
                        AuthorId = 4
                    },
                    new Book
                    {
                        Title = "Bookkkk",
                        Year = 2011,
                        GenreId = 6,
                        AuthorId = 2
                    },
                    new Book
                    {
                        Title = "bookkksss",
                        Year = 2012,
                        GenreId = 5,
                        AuthorId = 2
                    },
                    new Book
                    {
                        Title = "BookCbb",
                        Year = 2002,
                        GenreId = 1,
                        AuthorId = 6
                    }

                    );
                context.SaveChanges();

            }

            
            




        }


    }
}
