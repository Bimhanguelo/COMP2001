using APIApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace APIApplication.Controllers
{
    public class ListController : Controller
    {
        private readonly APIAPPContext _context;

        public ListController(APIAPPContext context)
        {
            this._context = context;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetLists()
        {
            var Lists = await _context.Lists.ToListAsync();
            return Lists;
        }

        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetListById(int id)
        {
            var Lists = await _context.Lists.FindAsync(id);

            if (Lists == null)
            {
                return NotFound();
            }

            return Lists;
        }

        // GET: /Lists
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                ViewBag.UserNameSession = HttpContext.Session.GetString("UserNameSession").ToString();

                var ListsWithUsers = await _context.Lists.Include(l => l.ListUser).ToListAsync();
                return View(ListsWithUsers);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }

        // Inside your ListController
        [HttpGet]
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


        // POST: /List/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListId, ListUserId, ListName")] List list)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(list);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine(ex.Message);
                throw;
            }

            // If the ModelState is invalid, return to the Create view with the model
            return View(list);
        }

        // GET: /List/Edit/5
        [HttpGet]
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

            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        // POST: /List/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListId, ListUserId, ListName")] List list)
        {
            if (id != list.ListId)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(list);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(list.ListId))
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
                // Log the exception or handle it appropriately
                Console.WriteLine(ex.Message);
                throw;
            }

            // If ModelState is invalid or an exception occurred, return to the Edit view with the model
            return View(list);
        }



        // GET: /Lists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var List = await _context.Lists
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (List == null)
            {
                return NotFound();
            }

            return View(List);
        }

        // POST: /Lists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var List = await _context.Lists.FindAsync(id);
            _context.Lists.Remove(List);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Lists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var List = await _context.Lists.FirstOrDefaultAsync(m => m.ListId == id);

            if (List == null)
            {
                return NotFound();
            }

            return View(List);
        }

        private bool ListExists(int id)
        {
            return _context.Lists.Any(e => e.ListId == id);
        }
    }
}
