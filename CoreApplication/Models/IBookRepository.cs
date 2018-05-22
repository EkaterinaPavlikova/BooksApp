using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }
        Book Get(int id);
        void Create(Book book);
        void Update(Book book);
        void Update(List<Book> books);
        void Delete(int id);
        void Save();

    }
}
