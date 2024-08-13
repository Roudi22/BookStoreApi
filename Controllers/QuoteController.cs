// Controllers/QuoteController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookStoreApi.Services;
using BookStoreApi.Models;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly QuoteService _quoteService;

        public QuoteController(QuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddQuote([FromBody] AddQuoteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var quote = _quoteService.AddQuote(userId, request.Text);

            return Ok(quote);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUserQuotes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var quotes = _quoteService.GetUserQuotes(userId);
            return Ok(quotes);
        }
    }
}
