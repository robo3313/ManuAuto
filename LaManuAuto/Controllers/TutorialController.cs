using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaManuAuto.Data;
using LaManuAuto.Models;
using System.Security.Claims;

namespace LaManuAuto.Controllers
{
    public class TutorialController : Controller
    {
        private readonly LaManuAutoContext _context;

        public TutorialController(LaManuAutoContext context)
        {
            _context = context;
        }

        // GET: Tutorials
        [Authorize(Policy = "RequireAdminOrManagerOrUser")]
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            string userId = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;

            List<TutorialView> tmp = _context.TutorialView.Where(t => t.UserId == userId).ToList();

            List<Tutorial> tutorials = _context.Tutorials.ToList();

            foreach (Tutorial tutorial in tutorials)
            {
                foreach (TutorialView tutorialView in tmp)
                {
                    if (tutorialView.TutorialId == tutorial.Id)
                    {
                        tutorial.Viewed = true;
                    }
                }
            }

            return View(tutorials);
        }

        // GET: Tutorials/Details/5
        [Authorize(Policy = "RequireAdminOrManager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tutorials == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials.Include(t => t.Tags).FirstOrDefaultAsync(t => t.Id == id);
            if (tutorial == null)
            {
                return NotFound();
            }
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            string userId = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            TutorialView? view = _context.TutorialView.Where(t => t.TutorialId == id && t.UserId == userId).SingleOrDefault();
            if (view == null) {
                var tmp = new TutorialView();
                tmp.TutorialId = tutorial.Id;
                tmp.UserId = userId;
                _context.TutorialView.Add(tmp);
                await _context.SaveChangesAsync();
            }
            return View(tutorial);
        }

        // GET: Tutorials/Create
        [Authorize(Policy = "RequireAdminOrManager")]
        public IActionResult Create()
        {
            ViewData["Tags"] = _context.Tags;
            return View();
        }

        // POST: Tutorials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManager")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,VideoUrl,CreationDate,ModificationDate")] Tutorial tutorial)
        {
            ICollection<string> keys = HttpContext.Request.Form.Keys;
            List<int> tagsToAdd = new();
            foreach (string key in keys)
            {
                if (key.Contains("Tag_"))
                {
                    tagsToAdd.Add(Int32.Parse(HttpContext.Request.Form[key].ToString()));
                }
            }

            tutorial.Tags = _context.Tags.Where(e => tagsToAdd.Contains(e.Id)).ToList();
            tutorial.CreationDate = DateTime.Now;
            tutorial.ModificationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(tutorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorial);
        }

        // GET: Tutorials/Edit/5
        [Authorize(Policy = "RequireAdminOrManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tutorials == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials.Include(t => t.Tags).FirstOrDefaultAsync(t => t.Id == id);
            if (tutorial == null)
            {
                return NotFound();
            }
            ViewData["Tags"] = _context.Tags;
            return View(tutorial);
        }

        // POST: Tutorials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,VideoUrl,CreationDate,ModificationDate")] Tutorial newTutorial)
        {
            if (id != newTutorial.Id)
            {
                return NotFound();
            }

            ICollection<string> keys = HttpContext.Request.Form.Keys;
            List<int> tagsToAdd = new();
            foreach (string key in keys)
            {
                if (key.Contains("Tag_"))
                {
                    tagsToAdd.Add(Int32.Parse(HttpContext.Request.Form[key].ToString()));
                }
            }

            var tutorial = _context.Tutorials.Include(t => t.Tags).Single(u => u.Id == id);
            if (tutorial.Tags != null)
            {
                tutorial.Tags.Clear();
            }
            tutorial.Tags = _context.Tags.Where(e => tagsToAdd.Contains(e.Id)).ToList();
            tutorial.Title = newTutorial.Title;
            tutorial.Description = newTutorial.Description;
            tutorial.VideoUrl = newTutorial.VideoUrl;
            tutorial.ModificationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorialExists(tutorial.Id))
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
            return View(tutorial);
        }

        // GET: Tutorials/Delete/5
        [Authorize(Policy = "RequireAdminOrManager")]
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

        // POST: Tutorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdminOrManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tutorials == null)
            {
                return Problem("Entity set 'LaManuAutoContext.Tutorials'  is null.");
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
