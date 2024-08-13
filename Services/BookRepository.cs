using BookStoreApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreApi.Services
{
    public class BookRepository
    {
        private readonly List<Book> _books = new();
        private int _nextId = 1;

        public IEnumerable<Book> GetAll() => _books;

        public Book GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

        public void Add(Book book)
        {
            book.Id = _nextId++;
            _books.Add(book);
        }

        public void Update(Book book)
        {
            var existing = GetById(book.Id);
            if (existing == null) return;

            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.ISBN = book.ISBN;
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }
    }
}
