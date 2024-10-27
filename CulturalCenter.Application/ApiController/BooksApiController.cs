using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulturalCenter.Application.ApiController
{
    using Microsoft.AspNetCore.Mvc;
    using ApplicationData.Repository;
    using Domain.Models;
    using System.IO;
    using System.Threading.Tasks;

    namespace CulturalCenter.Application.ApiControllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BooksApiController : ControllerBase
        {
            private readonly IRepository<Book> _bookRepository;

            public BooksApiController(IRepository<Book> bookRepository)
            {
                _bookRepository = bookRepository;
            }

            [HttpGet("download/{id}")]
            public async Task<IActionResult> DownloadBook(int id)
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null || string.IsNullOrEmpty(book.FilePath))
                {
                    return NotFound("Book or file not found.");
                }

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.FilePath);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                var fileName = Path.GetFileName(filePath);
                var contentType = "application/pdf"; 

                return File(fileBytes, contentType, fileName);
            }
        }
    }

}
