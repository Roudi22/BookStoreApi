// Models/Quote.cs
namespace BookStoreApi.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }  // To link the quote to a specific user
    }
}
