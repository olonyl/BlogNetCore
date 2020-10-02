using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Article> Articles{ get; set; }
    }
}
