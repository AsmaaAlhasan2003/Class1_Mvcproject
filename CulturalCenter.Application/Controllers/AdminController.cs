using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationData.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace CulturalCenter.Application.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Visitor> _visitorRepository;
        private readonly IRepository<Exhibition> _exhibitionRepository;
        private readonly IRepository<Loan> _loanRepository;

        public AdminController(
            IRepository<Book> bookRepository,
            IRepository<Author> authorRepository,
            IRepository<Visitor> visitorRepository,
            IRepository<Exhibition> exhibitionRepository,
            IRepository<Loan> loanRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _visitorRepository = visitorRepository;
            _exhibitionRepository = exhibitionRepository;
            _loanRepository = loanRepository;
        }

        public async Task<IActionResult> Dashboard()
        {
            var totalBooks = (await _bookRepository.GetAllAsync()).Count;
            var totalAuthors = (await _authorRepository.GetAllAsync()).Count;
            var totalVisitors = (await _visitorRepository.GetAllAsync()).Count;
            var totalExhibitions = (await _exhibitionRepository.GetAllAsync()).Count;

            ViewBag.TotalBooks = totalBooks;
            ViewBag.TotalAuthors = totalAuthors;
            ViewBag.TotalVisitors = totalVisitors;
            ViewBag.TotalExhibitions = totalExhibitions;

            return View();
        }

        public async Task<IActionResult> ManageBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return View(books);
        }

        public async Task<IActionResult> ManageAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();
            return View(authors);
        }

        public async Task<IActionResult> ManageVisitors()
        {
            var visitors = await _visitorRepository.GetAllAsync();
            return View(visitors);
        }

        public async Task<IActionResult> ManageExhibitions()
        {
            var exhibitions = await _exhibitionRepository.GetAllAsync();
            return View(exhibitions);
        }

        public async Task<IActionResult> ManageLoans()
        {
            var loans = await _loanRepository.GetAllAsync();
            return View(loans);
        }
    }
}
