// Data/DataContext.cs
using System.Collections.Generic;
using BookStoreApi.Models;


namespace BookStoreApi.Data
{
    public class DataContext
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Quote> Quotes { get; set; } = new List<Quote>(); // Add this line
    }
}
