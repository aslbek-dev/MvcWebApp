using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcWeb.Data;
using MvcWeb.Models;

namespace MvcWeb.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        private readonly AppDbContext _dbContext;

        public UserManagementController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
    public IActionResult Index()
    {
        var currentUser = _dbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
        
        if (currentUser != null && currentUser.Status == UserStatus.Blocked)
        {
            return RedirectToAction("AccessDenied", "Account");
        }
        
        var users = _dbContext.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> BlockUsers(string[] selectedUsers)
    {
        if (selectedUsers != null)
        {
            foreach (var userId in selectedUsers)
            {
                var user = await _dbContext.Users.FindAsync(userId);
                if (user != null)
                {
                    user.Status = UserStatus.Blocked;
                }
            }

            await _dbContext.SaveChangesAsync();
        }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUsers(string[] selectedUsers)
        {
            if (selectedUsers != null)
            {
                foreach (var userId in selectedUsers)
                {
                    var user = await _dbContext.Users.FindAsync(userId);
                    if (user != null)
                    {
                        user.Status = UserStatus.Active;
                    }
                }

                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUsers(string[] selectedUsers)
        {
            if (selectedUsers != null)
            {
                foreach (var userId in selectedUsers)
                {
                    var user = await _dbContext.Users.FindAsync(userId);
                    if (user != null)
                    {
                        _dbContext.Users.Remove(user);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
