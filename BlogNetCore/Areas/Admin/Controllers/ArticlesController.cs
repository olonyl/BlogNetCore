using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Models;
using BlogNetCore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ArticlesController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
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
            var artilceVM = new ArticleVM()
            {
                Article = new Article(),
                CategoryList = unitOfWork.Category.GetCategoryList()
            };
            return View(artilceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM artilceVM)
        {
            if (ModelState.IsValid)
            {
                string mainURL = webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (artilceVM.Article.Id == 0)
                {
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var upload = Path.Combine(mainURL, @"images\articles");
                        var ext = Path.GetExtension(files[0].FileName);
                        using (var fileStreams = new FileStream(Path.Combine(upload, $"{fileName}{ext}"), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        artilceVM.Article.UrlImage = $@"\images\articles\{fileName}{ext}";

                    }
                    artilceVM.Article.CreationDate = DateTime.Now.ToString();
                    unitOfWork.Article.Add(artilceVM.Article);
                    unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            artilceVM.CategoryList = unitOfWork.Category.GetCategoryList();
            return View(artilceVM);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var artilceVM = new ArticleVM()
            {
                Article = new Article(),
                CategoryList = unitOfWork.Category.GetCategoryList()
            };
            if (id == null) return NotFound();
            var article= unitOfWork.Article.Get(id.Value);
            if (article == null)
                return NotFound();
            artilceVM.Article = article;
            return View(artilceVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM artilceVM)
        {
            if (ModelState.IsValid)
            {
                string mainURL = webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var article = unitOfWork.Article.Get(artilceVM.Article.Id);

                if (files.Count>0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(mainURL, @"images\articles");

                    var newExt = Path.GetExtension(files[0].FileName);
                    var fixedURL = article.UrlImage != null ? article.UrlImage : String.Empty;
                    var editImageLocationURL = Path.Combine(mainURL, fixedURL.TrimStart('\\'));

                    if (System.IO.File.Exists(editImageLocationURL))
                        System.IO.File.Delete(editImageLocationURL);

                    using (var fileStreams = new FileStream(Path.Combine(upload, $"{fileName}{newExt}"), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    artilceVM.Article.UrlImage = $@"\images\articles\{fileName}{newExt}";
                 }
                else
                    artilceVM.Article.UrlImage = article.UrlImage;
                artilceVM.Article.CreationDate = article.CreationDate;

                unitOfWork.Article.Update(artilceVM.Article);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
         
        }
        #region Call to API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = unitOfWork.Article.GetAll(includeProperties:"Category") });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var article = unitOfWork.Article.Get(id);
            string mainURL = webHostEnvironment.WebRootPath;
        
            if (article == null) return Json(new { success = false, message = "Error deleting article" });

         
            unitOfWork.Article.Remove(article);
            unitOfWork.Save();

            if (article.UrlImage != null)
            {
                var editImageLocationURL = Path.Combine(mainURL, article.UrlImage.TrimStart('\\'));
                if (System.IO.File.Exists(editImageLocationURL))
                    System.IO.File.Delete(editImageLocationURL);
            }

            return Json(new { success = true, message = "Article deleted successfuly" });

        }
        #endregion
    }
}