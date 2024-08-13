using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookRepository _repository;

        public BooksController(BookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get() => Ok(new { success = "Request successful.", Books = _repository.GetAll() });

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _repository.GetById(id);
            return book == null ? NotFound(new { Message = "Book not found." }) : (ActionResult<Book>)Ok(new { success = "Request successful.", Book = book });
        }

        [Authorize] // Requires authentication
        [HttpPost] // POST api/books/create-book
        public ActionResult Add([FromBody] Book book)
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return Unauthorized(new { Message = "User is not authenticated." });
            }
            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
            {
                return BadRequest(new { Message = "Title and Author are required." });
            }

            _repository.Add(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, new { Message = "Book added successfully.", Book = book });
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest(new { Message = "ID mismatch." });
            }

            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
            {
                return BadRequest(new { Message = "Title and Author are required." });
            }

            var existingBook = _repository.GetById(id);
            if (existingBook == null)
            {
                return NotFound(new { Message = "Book not found." });
            }

            _repository.Update(book);
            return Ok(new{message = "Book updated successfully"}); // 204 No Content response
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingBook = _repository.GetById(id);
            if (existingBook == null)
            {
                return NotFound(new { Message = "Book not found." });
            }

            _repository.Delete(id);
            return Ok(new { Message = "Book deleted successfully." });
        }
    }
}
