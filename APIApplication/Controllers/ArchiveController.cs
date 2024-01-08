using APIApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIApplication.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly APIAPPContext _context;

        public ArchiveController(APIAPPContext context)
        {
            this._context = context;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archive>>> GetArchives()
        {
            var Archives = await _context.Archives.ToListAsync();
            return Archives;
        }

        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Archive>> GetArchiveById(int id)
        {
            var Archive = await _context.Archives.FindAsync(id);

            if (Archive == null)
            {
                return NotFound();
            }

            return Archive;
        }

        // GET: /Archive
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                ViewBag.UserNameSession = HttpContext.Session.GetString("UserNameSession").ToString();

                var ArchivesWithUsers = await _context.Archives.Include(b => b.ArchiveUser).ToListAsync();
                return View(ArchivesWithUsers);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }

        // GET: /Archive/Create
        public IActionResult Create()
        {
            // Fetch the list of users from UserProfile model
            var users = _context.UserProfiles.ToList();

            // Create a SelectList for the dropdown
            var usersSelectList = new SelectList(users, "UserId", "UserName");

            // Pass the SelectList to the ViewData instead of ViewBag
            ViewData["UsersSelectList"] = usersSelectList;

            return View();
        }

        // POST: /Archive/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArchiveId, ArchiveUserId, ArchiveDate")] Archive Archive)
        {
            try
            {
                _context.Add(Archive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // GET: /Archive/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Fetch the list of users from UserProfile model
            var users = _context.UserProfiles.ToList();

            // Create a SelectList for the dropdown
            var usersSelectList = new SelectList(users, "UserId", "UserName");

            // Pass the SelectList to the ViewData instead of ViewBag
            ViewData["UsersSelectList"] = usersSelectList;

            if (id == null)
            {
                return NotFound();
            }

            var Archive = await _context.Archives.FindAsync(id);
            if (Archive == null)
            {
                return NotFound();
            }
            return View(Archive);
        }

        // POST: /Archive/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArchiveId, ArchiveUserId, ArchiveDate")] Archive Archive)
        {
            if (id != Archive.ArchiveId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(Archive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchiveExists(Archive.ArchiveId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // GET: /Archive/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Archive = await _context.Archives
                .FirstOrDefaultAsync(m => m.ArchiveId == id);
            if (Archive == null)
            {
                return NotFound();
            }

            return View(Archive);
        }

        // POST: /Archive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Archive = await _context.Archives.FindAsync(id);
            _context.Archives.Remove(Archive);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Archive/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Archive = await _context.Archives.FirstOrDefaultAsync(m => m.ArchiveId == id);

            if (Archive == null)
            {
                return NotFound();
            }

            return View(Archive);
        }

        private bool ArchiveExists(int id)
        {
            return _context.Archives.Any(e => e.ArchiveId == id);
        }
    }
}
