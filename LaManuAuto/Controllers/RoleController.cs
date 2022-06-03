using LaManuAuto.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaManuAuto.Controllers
{
    public class RoleController : Controller
    {
        private readonly LaManuAutoContext _context;
        public RoleController(LaManuAutoContext context) => _context = context;
        public async Task<IActionResult> Index()
        {
            return _context.Roles != null ?
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'LaManuAutoContext.Roles'  is null.");
        }

        [Authorize(Policy = "RequireManager")]
        public async Task<IActionResult> Manager()
        {
            return _context.Roles != null ?
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'LaManuAutoContext.Roles'  is null.");
        }

        [Authorize(Policy = "RequireAdmin")]
        //[Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Manager}")]
        public async Task<IActionResult> Admin()
        {
            return _context.Roles != null ?
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'LaManuAutoContext.Roles'  is null.");
        }
    }
}
