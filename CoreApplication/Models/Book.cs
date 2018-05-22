using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    [JsonObject(IsReference = true)]
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Range(1000, 2018, ErrorMessage = "Год должен быть в промежутке от 1000 до 2018") ]       
        public int Year { get; set; }

        public int? AuthorId { get; set; }
        public Author Author { get; set; }

        public int? GenreId { get; set; }
        public Genre Genre { get; set; }

        public int Count { get; set; }

    }
}
