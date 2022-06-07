using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaManuAuto.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "RequireManager")]
        public IActionResult Manager()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdmin,RequireManager")]
        [Authorize(Roles = "RequireAdmin,RequireManager")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
