using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
