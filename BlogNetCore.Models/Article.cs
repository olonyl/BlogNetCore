using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogNetCore.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="The name is required")]
        [Display(Name ="Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "URL Image")]
        public string UrlImage { get; set; }

        [Required]
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category{ get; set; }
    }
}
