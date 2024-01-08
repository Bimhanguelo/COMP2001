using APIApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIApplication.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly APIAPPContext _context;

        public UserProfileController(APIAPPContext context)
        {
            this._context = context;
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            var UserProfiles = await _context.UserProfiles.ToListAsync();
            return UserProfiles;
        }

        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfileById(int id)
        {
            var UserProfile = await _context.UserProfiles.FindAsync(id);

            if (UserProfile == null)
            {
                return NotFound();
            }

            return UserProfile;
        }

        // GET: /UserProfile
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("EmailSession") != null &&
                HttpContext.Session.GetString("PasswordSession") != null)
            {
                ViewBag.UserNameSession = HttpContext.Session.GetString("UserNameSession").ToString();

                var UserProfiles = await _context.UserProfiles.ToListAsync();
                return View(UserProfiles);
            }
            else
            {
                return RedirectToAction("Login", "Home"); // Redirect to the login action if session variables are not set
            }
        }

        // GET: /UserProfile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /UserProfile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,EmailAddress,Description,PictureUrl,Difficulty,Location,JoinedDate,Units,Password,Height,Weight,Birthday,Language,ActivityTimeframe,Activities,Archives,Deletes,Lists")] UserProfile userProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(userProfile);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception accordingly
                Console.WriteLine(ex.Message);
                throw;
            }

            return View(userProfile);
        }


        // GET: /UserProfile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return View(userProfile);
        }
        // POST: /UserProfile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,EmailAddress,Description,PictureUrl,Difficulty,Location,JoinedDate,Units,Password,Height,Weight,Birthday,Language,ActivityTimeframe,Activities,Archives,Deletes,Lists")] UserProfile userProfile)
        {

            if (id != userProfile.UserId)
            {
                return NotFound();
            }

            try
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log or print the exception message for debugging purposes
                Console.WriteLine(ex.Message);
                throw; // Rethrow the exception or handle it accordingly
            }
        }


        // GET: /UserProfile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: /UserProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id);
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /UserProfile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(m => m.UserId == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.UserId == id);
        }
    }
}
