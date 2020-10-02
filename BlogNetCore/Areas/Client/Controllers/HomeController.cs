using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogNetCore.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Models.ViewModels;

namespace BlogNetCore.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Articles = unitOfWork.Article.GetAll(),
                Sliders = unitOfWork.Slider.GetAll()
            };
            return View(homeVM);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var article = unitOfWork.Article.GetFirstOrDefault(a=> a.Id == id);
            if (article == null) return NotFound();
            return View(article);
        }
    }
}
