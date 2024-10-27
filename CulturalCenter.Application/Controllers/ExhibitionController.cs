using ApplicationData.Repository;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulturalCenter.Application.Controllers
{
    public class ExhibitionController : Controller
    {
        private readonly IRepository<Exhibition> _exhibitionRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Visitor> _visitorRepository;
        private readonly ILogger<ExhibitionController> _logger;

        public ExhibitionController(
            IRepository<Exhibition> exhibitionRepository,
            IRepository<Book> bookRepository,
            IRepository<Visitor> visitorRepository,
            ILogger<ExhibitionController> logger)
        {
            _exhibitionRepository = exhibitionRepository;
            _bookRepository = bookRepository;
            _visitorRepository = visitorRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var exhibitions = await _exhibitionRepository.GetAllAsync();
                return View(exhibitions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching exhibitions.");
                ModelState.AddModelError("", "Error fetching exhibitions.");
                return View(new List<Exhibition>());
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Books = new SelectList(await _bookRepository.GetAllAsync(), "Id", "Name");
                ViewBag.Visitors = new SelectList(await _visitorRepository.GetAllAsync(), "VisitorId", "Name");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error preparing data for exhibition creation.");
                ModelState.AddModelError("", "Error preparing data.");
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exhibition exhibition, List<int> selectedBooks, List<int> selectedVisitors)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    exhibition.books = (await _bookRepository.GetAllAsync()).Where(b => selectedBooks.Contains(b.Id)).ToList();
                    exhibition.visitors = (await _visitorRepository.GetAllAsync()).Where(v => selectedVisitors.Contains((int)v.VisitorId)).ToList();

                    await _exhibitionRepository.AddAsync(exhibition);
                    await _exhibitionRepository.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Exhibition has been added successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating exhibition.");
                ModelState.AddModelError("", "Error adding exhibition.");
            }

            ViewBag.Books = new SelectList(await _bookRepository.GetAllAsync(), "Id", "Name");
            ViewBag.Visitors = new SelectList(await _visitorRepository.GetAllAsync(), "VisitorId", "Name");

            return View(exhibition);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var exhibition = await _exhibitionRepository.GetByIdAsync(id);
            if (exhibition == null)
            {
                return NotFound();
            }

            return View(exhibition);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var exhibition = await _exhibitionRepository.GetByIdAsync(id);
                if (exhibition == null)
                {
                    return NotFound();
                }

                await _exhibitionRepository.DeleteAsync(id);
                await _exhibitionRepository.SaveChangesAsync();
                TempData["SuccessMessage"] = "Exhibition has been deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error deleting exhibition.";
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
