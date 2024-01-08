using APIApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace APIApplication.Controllers
{
    public class ActivityController : Controller
    {
        private readonly APIAPPContext _context;

        public ActivityController(APIAPPContext context)
        {
            this._context = context;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            var Activities = await _context.Activities.ToListAsync();
            return Activities;
        }

        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityById(int id)
        {
            var user = await _context.Activities.FindAsync(id);

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

                var ActivitiesWithUsers = await _context.Activities.Include(a => a.ActivityUser).ToListAsync();
                return View(ActivitiesWithUsers);
            }
            else
            {
                return RedirectToAction("Login", "Home");
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
        // POST: /Activity/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId, ActivityUserId, ActivityType, ActivityDate")] Activity activity)
        {
            try
            {
                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // GET: /Activity/Edit/5
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

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        // POST: /Activity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityId, ActivityUserId, ActivityType, ActivityDate")] Activity activity)
        {
            if (id != activity.ActivityId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(activity.ActivityId))
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

            var Activity = await _context.Activities
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (Activity == null)
            {
                return NotFound();
            }

            return View(Activity);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Activity = await _context.Activities.FindAsync(id);
            _context.Activities.Remove(Activity);
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

            var Activity = await _context.Activities.FirstOrDefaultAsync(m => m.ActivityId == id);

            if (Activity == null)
            {
                return NotFound();
            }

            return View(Activity);
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.ActivityId == id);
        }
    }
}
