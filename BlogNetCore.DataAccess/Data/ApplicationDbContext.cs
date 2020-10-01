using System;
using System.Collections.Generic;
using System.Text;
using BlogNetCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogNetCore.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Article> Article{ get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
