using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public interface IAuthorRepository
    {
        IQueryable<Author> Author { get; }
    }
}
