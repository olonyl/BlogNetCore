using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = CNT.Admin)]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]        
        public IActionResult Index()

        {
            var claimsIndentity = (ClaimsIdentity)this.User.Identity;
            var currentUser = claimsIndentity.FindFirst(ClaimTypes.NameIdentifier);
            var userName = string.Empty;
            if (currentUser != null)
                userName = currentUser.Value.ToString();

            return View(unitOfWork.User.GetAll(u=> u.Id != userName));
        }
        [HttpGet]
        public IActionResult Lock(string userId)
        {
            if (userId == null)
                return NotFound();
            unitOfWork.User.LockUser(userId);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Unlock(string userId)
        {
            if (userId == null)
                return NotFound();
            unitOfWork.User.UnlockUser(userId);
            return RedirectToAction(nameof(Index));

        }
    }
}