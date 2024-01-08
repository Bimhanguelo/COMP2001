using APIApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace APIApplication.Controllers
{
    public class DeleteController : Controller
    {
        private readonly APIAPPContext _context;

        public DeleteController(APIAPPContext context)
        {
            this._context = context;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delete>>> GetDeletes()
        {
            var Deletes = await _context.Deletes.ToListAsync();
            return Deletes;
        }

        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Delete>> GetDeleteById(int id)
        {
            var user = await _context.Deletes.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: /User
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                ViewBag.UserNameSession = HttpContext.Session.GetString("UserNameSession").ToString();

                var DeletesWithUsers = await _context.Deletes.Include(b => b.RequestUser).ToListAsync();
                return View(DeletesWithUsers);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }

        // GET: /User/Create
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
        // POST: /Delete/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId, RequestUserId, RequestDate")] Delete deleteEntry)
        {
            try
            {
                _context.Add(deleteEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        // GET: /Delete/Edit/5
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

            var Delete = await _context.Deletes.FindAsync(id);
            if (Delete == null)
            {
                return NotFound();
            }
            return View(Delete);
        }

        // POST: /Delete/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId, RequestUserId, RequestDate")] Delete deleteEntry)
        {
            if (id != deleteEntry.RequestId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(deleteEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeleteExists(deleteEntry.RequestId))
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




        // GET: /User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Delete = await _context.Deletes
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (Delete == null)
            {
                return NotFound();
            }

            return View(Delete);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Delete = await _context.Deletes.FindAsync(id);
            _context.Deletes.Remove(Delete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Delete = await _context.Deletes.FirstOrDefaultAsync(m => m.RequestId == id);

            if (Delete == null)
            {
                return NotFound();
            }

            return View(Delete);
        }

        private bool DeleteExists(int id)
        {
            return _context.Deletes.Any(e => e.RequestId == id);
        }
    }
}
