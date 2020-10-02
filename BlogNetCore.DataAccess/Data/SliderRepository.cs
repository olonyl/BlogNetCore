using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogNetCore.DataAccess.Data
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext db;
        public SliderRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public void Update(Slider slider)
        {
            var currentSlider = db.Slider.FirstOrDefault(f => f.Id == slider.Id);
            currentSlider.Name = slider.Name;
            currentSlider.Status = slider.Status;
            currentSlider.UrlImage = slider.UrlImage;
        }
    }
}
