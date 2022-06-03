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

namespace LaManuAuto.Controllers
{
    public class TutorialsController : Controller
    {
        private readonly LaManuAutoContext _context;

        public TutorialsController(LaManuAutoContext context)
        {
            _context = context;
        }

        // GET: Tutorials
        public async Task<IActionResult> Index()
        {
              return _context.Tutorials != null ? 
                          View(await _context.Tutorials.ToListAsync()) :
                          Problem("Entity set 'LaManuAutoContext.Tutorials'  is null.");
        }

        // GET: Tutorials/Details/5
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

        // GET: Tutorials/Create
        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tutorials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,VideoUrl,CreationDate,ModificationDate")] Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorial);
        }

        // GET: Tutorials/Edit/5
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

        // POST: Tutorials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,VideoUrl,CreationDate,ModificationDate")] Tutorial tutorial)
        {
            if (id != tutorial.Id)
            {
                return NotFound();
            }

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
