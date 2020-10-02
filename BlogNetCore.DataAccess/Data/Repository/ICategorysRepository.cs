using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.DataAccess.Data.Repository
{
    public interface ICategorysRepository: IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryList();
        void Update(Category category);
    }
}
