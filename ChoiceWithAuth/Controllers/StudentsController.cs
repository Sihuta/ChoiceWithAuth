using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChoiceWithAuth.Data;
using ChoiceWithAuth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ChoiceWithAuth.Controllers
{
    [Authorize(Policy = "Admin")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Name")] Student student)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new(student.Name);
                using var transaction = await _context.Database.BeginTransactionAsync();
                var result = await _userManager.CreateAsync(user, "123456");
                try
                {
                    if (result.Succeeded)
                    {
                        _context.Add(student);
                        await _context.SaveChangesAsync();

                        // add StudentId to the identity
                        Claim claim = new("StudentId", student.StudentId.ToString());
                        await _userManager.AddClaimAsync(user, claim);

                        transaction.Commit();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        transaction.Rollback();
                        ModelState.AddModelError("Name", result.Errors.First().Description);
                    }
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("Name", e.Message);
                }
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Name")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students' is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(student.Name);
            using var transaction = await _context.Database.BeginTransactionAsync();
            var result = await _userManager.DeleteAsync(user);

            try
            {
                if (result.Succeeded)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                ModelState.AddModelError("Error", ex.Message);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
