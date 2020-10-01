using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogNetCore.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Input a name for this category")]
        [Display(Name ="Category")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Visualization Order")]
        public int Sort { get; set; }
    }
}
