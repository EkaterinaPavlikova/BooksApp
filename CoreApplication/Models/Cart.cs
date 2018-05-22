using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApplication.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Book book, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Book.Id == book.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Book book)
        {
            CartLine line = lineCollection.Where(b => b.Book.Id == book.Id).FirstOrDefault();
            if (line.Quantity > 1)
            {
                line.Quantity--;
            }
            else
            {
                lineCollection.Remove(line);
                //lineCollection.RemoveAll(r => r.Book.Id == book.Id);
            }
            
           
        }

        public virtual int ComputeTotalNumber() => lineCollection.Sum(l => l.Quantity);
        
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Line => lineCollection;


    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }

    }
}
