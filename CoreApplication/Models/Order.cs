using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace CoreApplication.Models
{
    public class Order
    {
       
        public int OrderId { get; set; }
        
        public string ReaderId { get; set; }
       
        public int BookId { get; set; }
        public Book Book { get; set; }

        public BookStatus Status { get; set; }

    }

    public enum BookStatus
    {
       Processing,
       Issued,
       OnReturn
    }
}
