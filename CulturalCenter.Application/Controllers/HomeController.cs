using ApplicationData.Repository;
using CulturalCenter.Application.Models;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CulturalCenter.Application.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Book> _bookRepository; 
        private readonly IRepository<Exhibition> _exhibitionrepository;
        // حقن ILogger و IBookRepository
        public HomeController(ILogger<HomeController> logger,
            IRepository<Book> bookRepository,
            IRepository<Exhibition> exhibitionrepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
            _exhibitionrepository = exhibitionrepository;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var books = await _bookRepository.GetAllAsync();
            var exhibitions =await _exhibitionrepository.GetAllAsync();
            return View(new { Books = books,Exhibitions= exhibitions });
        }

        // السماح للجميع بالوصول إلى صفحة تسجيل الدخول
        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        

    }

    }

