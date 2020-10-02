using BlogNetCore.DataAccess.Data.Repository;
using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogNetCore.DataAccess.Data
{
    public class CategoryRepository : Repository<Category>, ICategorysRepository
    {
        private readonly ApplicationDbContext db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
        public IEnumerable<SelectListItem> GetCategoryList()
        {
            return db.Category.Select(i=> new SelectListItem() { 
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var currentCategory = db.Category.FirstOrDefault(s => s.Id == category.Id);
            currentCategory.Name = category.Name;
            currentCategory.Sort = category.Sort;

        }
    }
}
