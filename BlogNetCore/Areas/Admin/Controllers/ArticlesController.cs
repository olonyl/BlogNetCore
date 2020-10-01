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
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(mainURL,@"images\articles");
                    var ext = Path.GetExtension(files[0].FileName);
                    using (var fileStreams = new FileStream(Path.Combine(upload, $"{fileName}{ext}"), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    artilceVM.Article.UrlImage = $@"images\articles{fileName}{ext}";
                    artilceVM.Article.CreationDate = DateTime.Now.ToString();
                    unitOfWork.Article.Add(artilceVM.Article);
                    unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        #region Call to API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = unitOfWork.Article.GetAll(includeProperties:"Category") });
        }
        #endregion
    }
}