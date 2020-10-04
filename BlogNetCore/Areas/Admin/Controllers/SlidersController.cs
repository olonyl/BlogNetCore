using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SlidersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SlidersController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
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
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string mainURL = webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                string fileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(mainURL, @"images\sliders");
                if (files.Count > 0)
                {
                    var ext = Path.GetExtension(files[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(upload, $"{fileName}{ext}"), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    slider.UrlImage = $@"\images\sliders\{fileName}{ext}";
                }
                    unitOfWork.Slider.Add(slider);
                    unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var slider = unitOfWork.Slider.Get(id.Value);
            if (slider == null)
                return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string mainURL = webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var currentSlider = unitOfWork.Slider.Get(slider.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(mainURL, @"images\sliders");

                    var newExt = Path.GetExtension(files[0].FileName);
                    var fixedURL = currentSlider.UrlImage!=null? currentSlider.UrlImage : String.Empty;
                    var editImageLocationURL = Path.Combine(mainURL, fixedURL.TrimStart('\\'));

                    if (System.IO.File.Exists(editImageLocationURL))
                        System.IO.File.Delete(editImageLocationURL);

                    using (var fileStreams = new FileStream(Path.Combine(upload, $"{fileName}{newExt}"), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    slider.UrlImage = $@"\images\sliders\{fileName}{newExt}";
                }
                else
                    currentSlider.UrlImage = slider.UrlImage;

                unitOfWork.Slider.Update(slider);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        #region Call to API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = unitOfWork.Slider.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var currentSlider = unitOfWork.Slider.Get(id);
            string mainURL = webHostEnvironment.WebRootPath;
        
            if (currentSlider == null) return Json(new { success = false, message = "Error deleting slider" });

         
            unitOfWork.Slider.Remove(currentSlider);
            unitOfWork.Save();

            if (currentSlider.UrlImage != null)
            {
                var editImageLocationURL = Path.Combine(mainURL, currentSlider.UrlImage.TrimStart('\\'));
                if (System.IO.File.Exists(editImageLocationURL))
                    System.IO.File.Delete(editImageLocationURL);
            }

            return Json(new { success = true, message = "Slider deleted successfuly" });

        }
        #endregion
    }
}