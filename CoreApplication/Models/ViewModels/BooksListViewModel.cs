using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models.ViewModels
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public string SearchingString { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int? CurrentGenreId { get; set; }
    }
}
