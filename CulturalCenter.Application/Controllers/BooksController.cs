using ApplicationData.Repository;
using Domain.Enum;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace CulturalCenter.Application.Controllers
{
    public class BooksController : Controller
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Exhibition> _exhibitionRepository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(
            IRepository<Book> bookRepository,
            IRepository<Author> authorRepository,
            IRepository<Exhibition> exhibitionRepository,
            ILogger<BooksController> logger)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _exhibitionRepository = exhibitionRepository;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllAsync();
            return View(books);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var authors = await _authorRepository.GetAllAsync();
            ViewBag.Authors = new SelectList(authors, "AuthorId", "Name");

            var exhibitions = await _exhibitionRepository.GetAllAsync();
            ViewBag.Exhibitions = new SelectList(exhibitions, "ExhibitionId", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile file)
        {
          /*  var testBook = new Book
            {
                Name = "Test Book",
                AuthorId = 1,
                DateOfPublication = DateTime.Now,
                Price = 19.99,
                Section = BookSection.Science,
                Status = BookStatus.Available
            };

            await _bookRepository.AddAsync(testBook);
            await _bookRepository.SaveChangesAsync();*/

            if (ModelState.IsValid)
            {
                try
                {
         
                    if (file != null && file.Length > 0)
                    {
            
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BooksFiles");
                        Directory.CreateDirectory(uploadsFolder); 

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        book.FilePath = Path.Combine("BooksFiles", uniqueFileName);
                    }

                    await _bookRepository.AddAsync(book);
                    await _bookRepository.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Book has been added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while adding book.");
                    ModelState.AddModelError("", "Error adding book. Please try again.");
                }
            }

            var authors = await _authorRepository.GetAllAsync();
            ViewBag.Authors = new SelectList(authors, "AuthorId", "Name");

            var exhibitions = await _exhibitionRepository.GetAllAsync();
            ViewBag.Exhibitions = new SelectList(exhibitions, "ExhibitionId", "Name");

            return View(book);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteAsync(id);
            await _bookRepository.SaveChangesAsync();
            TempData["SuccessMessage"] = "Book has been deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm, string filterBy, string filterValue)
        {
            var books = (await _bookRepository.GetAllAsync()).AsQueryable();


            if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterValue))
            {
                books = filterBy switch
                {
                    "Author" => books.Where(b => b.Author.Name.Contains(filterValue)),
                    "Section" => books.Where(b => b.Section.ToString().Contains(filterValue)),
                    "Status" => books.Where(b => b.Status.ToString().Contains(filterValue)),
                    _ => books
                };
            }

            return View("Index", books.ToList());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddManualBook()
        {
            try
            {
                // إنشاء كتاب جديد وإضافة بيانات يدوية
                var book = new Book
                {
                    Name = "Introduction to AI",
                    AuthorId = 1, // افتراض أن المؤلف موجود بالفعل
                    DateOfPublication = new DateTime(2023, 10, 20),
                    Price = 99.99,
                    Section = Domain.Enum.BookSection.Science,
                    Status = Domain.Enum.BookStatus.Available,
                    FilePath = "BooksFiles/IntroductionToAI.pdf"
                };
                // إضافة الكتاب الجديد لقاعدة البيانات
                await _bookRepository.AddAsync(book);

                // عرض رسالة نجاح
                TempData["SuccessMessage"] = "Book has been added manually!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding book manually.");
                ModelState.AddModelError("", "Error adding book manually.");
                return View("Error"); // عرض صفحة خطأ أو إعادة التوجيه إلى صفحة أخرى
            }
        }
    }
}
    