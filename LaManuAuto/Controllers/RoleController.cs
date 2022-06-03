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

        [Authorize(Policy = "RequireAdmin")]
        //[Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Manager}")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
