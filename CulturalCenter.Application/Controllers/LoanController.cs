using Microsoft.AspNetCore.Mvc;
using ApplicationData.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CulturalCenter.Application.Controllers
{
    public class LoanController : Controller
    {
        private readonly IRepository<Loan> _loanRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Visitor> _visitorRepository;

        public LoanController(IRepository<Loan> loanRepository,
            IRepository<Book> bookRepository,
            IRepository<Visitor> visitorRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _visitorRepository = visitorRepository;
        }

        public async Task<IActionResult> Create(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            var visitors = await _visitorRepository.GetAllAsync();
            ViewBag.Book = book;
            ViewBag.Visitors = new SelectList(visitors, "VisitorId", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                var book = await _bookRepository.GetByIdAsync(loan.BookId);
                if (book == null)
                {
                    ModelState.AddModelError("", "Book not found.");
                    return View(loan);
                }

                var visitor = await _visitorRepository.GetByIdAsync(loan.VisitorId);
                if (visitor == null)
                {
                    ModelState.AddModelError("", "Visitor not found.");
                    return View(loan);
                }

                loan.BorrowDate = loan.BorrowDate == DateTime.MinValue ? DateTime.Now : loan.BorrowDate;
                await _loanRepository.AddAsync(loan);
                await _loanRepository.SaveChangesAsync();

                TempData["SuccessMessage"] = "Loan has been added successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Book = await _bookRepository.GetByIdAsync(loan.BookId);
            ViewBag.Visitors = new SelectList(await _visitorRepository.GetAllAsync(), "VisitorId", "Name");

            return View(loan);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _loanRepository.DeleteAsync(id);
            await _loanRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
