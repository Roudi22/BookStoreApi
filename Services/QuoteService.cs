// Services/QuoteService.cs
using BookStoreApi.Models;
using BookStoreApi.Data;
using System.Collections.Generic;

namespace BookStoreApi.Services
{
    public class QuoteService
    {
        private readonly DataContext _context;

        public QuoteService(DataContext context)
        {
            _context = context;
        }

        public Quote AddQuote(string userId, string text)
        {
            var newQuote = new Quote
            {
                Id = _context.Quotes.Count + 1,
                Text = text,
                UserId = userId
            };

            _context.Quotes.Add(newQuote);
            return newQuote;
        }

        public IEnumerable<Quote> GetUserQuotes(string userId)
        {
            return _context.Quotes.Where(q => q.UserId == userId);
        }
    }
}
