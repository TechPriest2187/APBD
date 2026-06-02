using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserPanel.Data;
using UserPanel.Models;

namespace UserPanel.Controllers
{
    [Authorize] 
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var notes = _context.UserNotes.Where(n => n.AppUserId == userId).ToList();
            
            return View(notes);
        }

        [HttpPost]
        public IActionResult AddNote(string title, string content)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var note = new UserNote
            {
                AppUserId = userId,
                Title = title,
                Content = content
            };

            _context.UserNotes.Add(note);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}