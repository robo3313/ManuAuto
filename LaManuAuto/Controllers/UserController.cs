using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LaManuAuto.Areas.Identity.Data;
using LaManuAuto.Data;
using LaManuAuto.Models;
using Microsoft.EntityFrameworkCore;

namespace LaManuAuto.Controllers;

public class UserController : Controller
{
    private readonly LaManuAutoContext _context;
    private readonly SignInManager<LaManuAutoUser> _signInManager;

    [BindProperty]
    public IList<LaManuAutoUserRoleView> model { get; set; } = new List<LaManuAutoUserRoleView>();

    public UserController(LaManuAutoContext context, SignInManager<LaManuAutoUser> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = _context.Users.ToList();

        foreach (LaManuAutoUser user in users)
        {
            LaManuAutoUserRoleView urv = new LaManuAutoUserRoleView()
            {
                User = user,
                Roles = await _signInManager.UserManager.GetRolesAsync(user)
            };

            model.Add(urv);
        }

        return View(model);
    }

    
    public async Task<IActionResult> Edit(string id)
    {
        var user = _context.Users.Find(id);



        if (user == null) {
            return NotFound();
        }

        var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

        var vm = new LaManuAutoUserRoleView()
        {
            User = user,
            Roles = userRoles
        };

        if (vm.Roles.Count() > 0)
        {
            ViewData["Roles"] = new SelectList(_context.Roles, "Name", "Name");
        }
        else
        {
            ViewData["Roles"] = new SelectList(_context.Roles, "Name", "Name", vm.Roles.First());
        }


        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> OnPostAsync(LaManuAutoUserRoleView data)
    {
        var user = _context.Users.Find(data.User.Id);
        if (user == null)
        {
            return NotFound();
        }

        var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

        await _signInManager.UserManager.RemoveFromRoleAsync(user, userRolesInDb.First());


        foreach (var role in data.Roles)
        {
            await _signInManager.UserManager.AddToRoleAsync(user, role);
        }

        return RedirectToAction("Index");
    }
}

