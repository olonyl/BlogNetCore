using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.Models.ViewModels
{
    public class ArticleVM
    {
        public Article Article{ get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
