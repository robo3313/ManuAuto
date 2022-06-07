using LaManuAuto.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            //var listIdUserPosition = _context.UserRoles.Where(user=>user.RoleId==id);
            ViewBag.userByPosition = _context.UserRoles.Where(user=>user.RoleId==id);
            // SELECT * FROM Users u JOIN UserRoles ur ON u.Id=ur.UserID WHERE ur.RoleID=id
            /*ViewBag.userByPosition = listIdUserPosition.Join(
                _context.Users,
                userRole=>userRole.UserId,
                users => users.Id,
                (userRole, users)
                    => new {
                        ID=users.Id,
                        firstname=users.FirstName,
                        lastname=users.LastName}
                    );*/
            return _context.Roles.Find(id) != null ?
                          View(_context.Roles.Find(id)) :
                          Problem($"Entity set 'LaManuAutoContext.Roles.Find({id})'  is null.");
        }

        [Authorize(Policy = "RequireManager")]
        public async Task<IActionResult> Manager()
        {
            return _context.Roles != null ?
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'LaManuAutoContext.Roles'  is null.");
        }

        //[Authorize(Policy = "RequireAdmin,RequireManager")]
        [Authorize(Roles = "RequireAdmin,RequireManager")]
        public async Task<IActionResult> Admin()
        {
            return _context.Roles != null ?
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'LaManuAutoContext.Roles'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string id)
        {
            return _context.Roles.Find(id) != null ?
                          View(_context.Roles.Find(id)) :
                          Problem($"Entity set 'LaManuAutoContext.Roles.Find({id})'  is null.");
        }
    }
}
