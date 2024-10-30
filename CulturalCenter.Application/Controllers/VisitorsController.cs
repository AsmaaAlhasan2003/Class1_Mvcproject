using ApplicationData.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class VisitorsController : Controller
{
    private readonly IRepository<Visitor> _visitorRepository;

    public VisitorsController(IRepository<Visitor> visitorRepository)
    {
        _visitorRepository = visitorRepository;
    }
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Index()
    {
        var visitors = await _visitorRepository.GetAllAsync();
        return View(visitors);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Visitor visitor)
    {
        if (ModelState.IsValid)
        {
            await _visitorRepository.AddAsync(visitor);
            await _visitorRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(visitor);
    }
}
