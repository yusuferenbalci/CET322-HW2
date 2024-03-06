using CetBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace CetBooks.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            FakeDB fakeDB = new FakeDB();
            var allbooks = fakeDB.GetAllBooks();

            return View(allbooks);
        }

        public IActionResult Detail(int? id)
        {
            if (!id.HasValue) return BadRequest();

            FakeDB db = new FakeDB();
            var book = db.GetBookById(id.Value);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);

        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                FakeDB db = new FakeDB();
                db.AddBook(book); 
                return RedirectToAction("Index");
            }
            return View(book);
        }


        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            FakeDB db = new FakeDB();
            var result = db.DeleteBook(id.Value);
            if (result)
            {

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
