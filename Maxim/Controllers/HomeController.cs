using Maxim.DAL;
using Maxim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maxim.Controllers
{
	public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
		{
            var data = await _context.Categories.Select(c => new Category
            {
                Name = c.Name,
                Id = c.Id,
                Description = c.Description
            }).ToListAsync();

            return View(data);
		}
	}
}
