using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RoleController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<ApplicationRole> roleManager,ILogger<RoleController> logger ,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.OrderBy(r =>r.Name).ToListAsync();

            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
               var result = await _roleManager.CreateAsync(applicationRole);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else 
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, "Invalid User Role");
            }
            return View(applicationRole);
        }

        public async Task<IActionResult> Details(string Id, string viewName = "Details")
        {
            if (Id is null)
                return NotFound();

            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
                return NotFound();

            return View(viewName, role);
        }

        public async Task<IActionResult> Update(string Id)
        {
            return await Details(Id, "Update");
        }
        [HttpPost]
        public async Task<IActionResult> Update(string Id, ApplicationRole AppRole)
        {
            if (Id != AppRole.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(Id);
                    role.Name = AppRole.Name;
                    role.NormalizedName = AppRole.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                }
            }
            return View(AppRole);
        }
        public async Task<IActionResult> Delete(string Id, ApplicationUser AppRole)
        {
            if (Id != AppRole.Id)
                return NotFound();

            try
            {
                var role = await _roleManager.FindByIdAsync(Id);

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                //ViewBag.Errors = result.Errors;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            
            if (role == null)
                return BadRequest();

            ViewBag.RoleId = roleId;
            ViewBag.RoleName = role.Name;

            var usersInRole = new List<UserInRoleViewModel>();

            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserName = user.UserName,
                    UserId = user.Id,
                };

                if(await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected  = true ;
                else
                    userInRole.IsSelected = false ;

                usersInRole.Add(userInRole);

            }

            return View(usersInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleViewModel> users , string roleId)
            {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return BadRequest();

            if(ModelState.IsValid)
            {
                foreach (var user   in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    
                    if (appUser != null)
                    {
                        if (user.IsSelected && !(await _userManager.IsInRoleAsync(appUser, role.Name)))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if(!user.IsSelected && (await _userManager.IsInRoleAsync(appUser, role.Name)))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
                return RedirectToAction(nameof(Update), new { id = roleId });
            }
            return View(users);
        }

    }
}
