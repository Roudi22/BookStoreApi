// Models/AddQuoteRequest.cs
using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models
{
    public class AddQuoteRequest
    {
        [Required(ErrorMessage = "The text field is required.")]
        public string Text { get; set; }
    }
}
