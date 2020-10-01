using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(category);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = unitOfWork.Category.Get(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(category);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        #region Call to API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = unitOfWork.Category.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = unitOfWork.Category.Get(id);
            if (category == null) return Json(new { success = false, message = "Error deleting category" });
            unitOfWork.Category.Remove(category);
            unitOfWork.Save();
            return Json(new { success = true, message = "Category deleted successfuly" });

        }
        #endregion
    }
}