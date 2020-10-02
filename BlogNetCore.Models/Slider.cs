using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogNetCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The name is required")]

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name="Active?")]
        public bool Status { get; set; }

        [DataType(DataType.ImageUrl)]
        [DefaultValue("")]
        [Display(Name = "URL Image")]
        public string UrlImage { get; set; }


    }
}
