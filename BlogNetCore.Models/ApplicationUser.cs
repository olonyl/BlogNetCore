using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogNetCore.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage ="City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }
}
