using COMP003B.Assignment5.Models;
using Microsoft.AspNetCore.Mvc;

namespace COMP003B.Assignment5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private List<Book> _books = new List<Book>();

        public BookController() {
            _books.Add(new Book { Id = 1, Name = "Into the Wild", Description = "Adventure", Author = "Jon Krakauer" });
            _books.Add(new Book { Id = 2, Name = "The Lightning Thief", Description = "Adventure/Magic", Author = "Rick Riordan" });
            _books.Add(new Book { Id = 3, Name = "The Sea of Monsters", Description = "Adventure/Magic", Author = "Rick Riordan" });
            _books.Add(new Book { Id = 4, Name = "The Titan's Curse", Description = "Adventure/Magic", Author = "Rick Riordan" });
            _books.Add(new Book { Id = 5, Name = "The Last Olympian", Description = "Adventure/Magic", Author = "Rick Riordan" });
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks() 
        {
            return _books;
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookbyId(int id) 
        {
            var book = _books.FirstOrDefault(v => v.Id == id);

            if (book == null) 
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            book.Id = _books.Max(v => v.Id) + 1;

            _books.Add(book);

            return CreatedAtAction(nameof(GetBookbyId), new { id = book.Id }, book);
        }

        [HttpPut]
        public ActionResult<Book> UpdateBook (int id, Book updatedBook)
        {
            var book = _books.Find(v => v.Id == id);

            if (book == null)
            {
                return BadRequest();
            }

            book.Name = updatedBook.Name;
            book.Description = updatedBook.Description;
            book.Author = updatedBook.Author;

            return NoContent();
            
        }

        [HttpDelete]
        public ActionResult DeleteBook(int id) 
        {
            var book = _books.Find(v => v.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            _books.Remove(book);

            return NoContent();
        }
    }
}
