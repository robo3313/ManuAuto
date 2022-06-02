using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using ManuAuto.Models;
using TutoIdentity.Data;

namespace ManuAuto.Controllers
{
    public class TutorialController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TutorialController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tutorial
        public async Task<IActionResult> Index()
        {
              return _context.Tutorials != null ? 
                          View(await _context.Tutorials.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tutorials'  is null.");
        }

        // GET: Tutorial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tutorials == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutorial == null)
            {
                return NotFound();
            }

            return View(tutorial);
        }

        // GET: Tutorial/Create
        public IActionResult Create()
        {
            ViewData["Tags"] = _context.Tags;
            return View();
        }

        // POST: Tutorial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,VideoUrl,AuthorId,CreationDate,ModificationDate,Tags")] Tutorial tutorial)
        {
            ICollection<string> keys = HttpContext.Request.Form.Keys;
            List<int> tagsToAdd = new();
            foreach (string key in keys)
            {
                if (key.Contains("Tag_")) {
                    tagsToAdd.Add(Int32.Parse(HttpContext.Request.Form[key].ToString()));
                }
            }

            tutorial.Tags = _context.Tags.Where(e => tagsToAdd.Contains(e.Id)).ToList();
            tutorial.CreationDate = DateTime.Now;
            tutorial.ModificationDate = tutorial.CreationDate;
            if (ModelState.IsValid)
            {
                _context.Add(tutorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorial);
        }

        // GET: Tutorial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tutorials == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials.FindAsync(id);
            if (tutorial == null)
            {
                return NotFound();
            }
            return View(tutorial);
        }

        // POST: Tutorial/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tutorial tutorial)
        {
            if (ModelState.IsValid) {
                try {
                    tutorial.ModificationDate = DateTime.Now;
                    _context.Update(tutorial);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tutorial);
        }

        // GET: Tutorial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tutorials == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutorial == null)
            {
                return NotFound();
            }

            return View(tutorial);
        }

        // POST: Tutorial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tutorials == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tutorials'  is null.");
            }
            var tutorial = await _context.Tutorials.FindAsync(id);
            if (tutorial != null)
            {
                _context.Tutorials.Remove(tutorial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorialExists(int id)
        {
          return (_context.Tutorials?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
